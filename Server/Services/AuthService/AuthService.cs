using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Novell.Directory.Ldap;
using PannonBlazor.Shared.Models.Entity;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models;

namespace PannonBlazor.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users!.Where(u => u.Username.Equals(username)).Include(u => u.Department).Include(u => u.Programme).FirstAsync();
            return user;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public string GetUserName() => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name);

        public async Task<ServiceResponse<string>> Login(UserLoginDto loginDto)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users!.Include(u => u.Roles).FirstOrDefaultAsync(x => x.Email.ToLower().Equals(loginDto.Username.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "Rossz felhasználónév vagy jelszó";
            }
            else if (!user.Roles.Any(r => r.Id.ToString() == loginDto.RoleId))
            {
                response.Success = false;
                response.Message = "A felhasználónak ez a szerepköre nem található";
            }
            else if (string.IsNullOrEmpty(user.LdapUid))
            {
                if (VerifyPasswordHash(loginDto.Password, user.PasswordHash!, user.PasswordSalt!))
                {
                    response.Data = CreateToken(user, loginDto.RoleId);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Rossz felhasználónév vagy jelszó";
                }
            }
            else
            {
                try
                {
                    using (var cn = new LdapConnection())
                    {
                        cn.Connect("ldap-ng.uni-pannon.hu", 389);
                        var userDn = $"uid={user.LdapUid},ou=employees,ou=people,ou=jarvis-prod,dc=uni-pannon,dc=hu";
                        cn.Bind(userDn, loginDto.Password);
                        response.Data = CreateToken(user, loginDto.RoleId);
                        cn.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Wrong credentials";
                }

            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists"
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.ProgrammeId = null;

            if (string.IsNullOrEmpty(user.LdapUid))
            {
                var ldapResult = await GetUserLdapUid(user.Email);
                if (ldapResult != null && ldapResult.Success)
                {
                    user.LdapUid = ldapResult.Data;
                }
            }

            await _context.Users!.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "Registration successful!" };
        }

        public async Task<ServiceResponse<string>> GetUserLdapUid(string email)
        {
            try
            {
                using (var cn = new LdapConnection())
                {
                    cn.Connect("ldap-ng.uni-pannon.hu", 389);
                    cn.Bind("cn=hr-it,ou=sysuser,dc=uni-pannon,dc=hu", Environment.GetEnvironmentVariable("LDAP_PASS"));
                    var lsc = cn.Search("ou=employees,ou=people,ou=jarvis-prod,dc=uni-pannon,dc=hu",
                        LdapConnection.ScopeSub,
                        $"(mail={email})"
                        , null, false);
                    var uid = "";
                    while (lsc.HasMore())
                    {
                        LdapEntry nextEntry = null;
                        try
                        {
                            nextEntry = lsc.Next();
                        }
                        catch (LdapException e)
                        {
                            // Exception is thrown, go for next entry
                            continue;
                        }

                        LdapAttributeSet attributeSet = nextEntry.GetAttributeSet();
                        System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();

                        while (ienum.MoveNext())
                        {
                            LdapAttribute attribute = (LdapAttribute)ienum.Current;
                            string attributeName = attribute.Name;
                            string attributeVal = attribute.StringValue;
                            if (attributeName.Equals("uid"))
                            {
                                uid = attributeVal;
                            }
                        }
                    }
                    cn.Disconnect();
                    return new ServiceResponse<string> { Data = uid, Success = true };
                }
            }
            catch (Exception)
            {
                return new ServiceResponse<string>();
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users!.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
                return true;
            return false;
        }

        private String CreateToken(User user, string roleId)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.SerialNumber, user.Id.ToString())
            };

            var role = _context.Roles!.FirstOrDefault(r => r.Id.ToString() == roleId);

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, ""));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
