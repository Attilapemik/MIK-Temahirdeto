global using Microsoft.EntityFrameworkCore;
global using PannonBlazor.Server.Data;
global using PannonBlazor.Shared;
global using PannonBlazor.Server.Services.AuthService;
global using PannonBlazor.Server.Services.ThemeService;
global using PannonBlazor.Server.Services.InstructorService;
global using PannonBlazor.Server.Services.ProgrammeService;
global using PannonBlazor.Server.Services.DepartmentService;
global using PannonBlazor.Server.Services.SemesterService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.StaticFiles;
using PannonBlazor.Server.Services.RoleService;
using PannonBlazor.Server.Services.CompanyService;
using PemikFramework.Core.UnitOfWork;
using PemikFramework.Core.Web.Util;
using PemikFramework.DocumentGenerator.DocumentGeneratorMessageQueue;
using PemikFramework.DocumentGenerator.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var provider = new FileExtensionContentTypeProvider();
builder.Services.AddSingleton<IMimeMappingService>(new MimeMappingService(provider));
builder.Services.AddScoped<IUnitOfWorkProvider>(uowServiceProvider =>
{
    var unitOfWorks = uowServiceProvider.GetServices<IUnitOfWork>().ToList();
    return new UnitOfWorkProvider(unitOfWorks, typeof(DataContext));
});
builder.Services.AddScoped<PemikFramework.Core.Services.IProcessService, PemikFramework.Core.Services.ProcessService>();
builder.Services.Configure<DocumentConfiguration>(builder.Configuration.GetSection("DocumentConfiguration"));
builder.Services.AddDocumentGeneratorServices();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IProgrammeService, ProgrammeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

//builder.Services.Configure<Saml2Configuration>(builder.Configuration.GetSection("Saml2"));
//builder.Services.Configure<Saml2Configuration>(saml2Configuration =>
//{
//    saml2Configuration.SigningCertificate = CertificateUtil.Load(builder.Environment.MapToPhysicalFilePath(
//        builder.Configuration["Saml2:SigningCertificateFile"]), builder.Configuration["Saml2:SigningCertificatePassword"]);
//    saml2Configuration.AllowedAudienceUris.Add(saml2Configuration.Issuer);

//    var entityDescriptor = new EntityDescriptor();
//    entityDescriptor.ReadIdPSsoDescriptorFromUrl(new Uri(builder.Configuration["Saml2:IdPMetadata"]));
//    if (entityDescriptor.IdPSsoDescriptor != null)
//    {
//        saml2Configuration.SingleSignOnDestination = entityDescriptor.IdPSsoDescriptor.SingleSignOnServices.First().Location;
//        saml2Configuration.SingleLogoutDestination = entityDescriptor.IdPSsoDescriptor.SingleLogoutServices.First().Location;
//        saml2Configuration.SignatureValidationCertificates.AddRange(entityDescriptor.IdPSsoDescriptor.SigningCertificates);
//    }
//    else
//    {
//        throw new Exception("IdPSsoDescriptor not loaded from metadata.");
//    }
//});
//builder.Services.AddSaml2();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjI5MzU3QDMyMzAyZTMxMmUzMEo2T3JrdlFjTERqOFpqaW9oZVdyWjB1WjA0ZExHVlBIMFJOc1dqWjAyZHM9");

#region Migrate DB

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<DataContext>();
    ctx.Database.Migrate();
}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
