
global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
global using PannonBlazor.Shared;
global using System.Net.Http.Json;
global using PannonBlazor.Client.Services.AuthService;
global using PannonBlazor.Client.Services.ThemeService;
global using PannonBlazor.Client.Services.InstructorService;
global using PannonBlazor.Client.Services.ProgrammeService;
global using PannonBlazor.Client.Services.DepartmentService;
global using PannonBlazor.Client.Services.SemesterService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PannonBlazor.Client;
using Blazored.Modal;
using Syncfusion.Blazor;
using PannonBlazor.Client.Services.RoleService;
using PannonBlazor.Client.Services.CompanyService;
using PannonBlazor.Client.Services.ComponentServices.Toast;
using PannonBlazor.Client.Services.ComponentServices;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjI5MzU3QDMyMzAyZTMxMmUzMEo2T3JrdlFjTERqOFpqaW9oZVdyWjB1WjA0ZExHVlBIMFJOc1dqWjAyZHM9");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IProgrammeService, ProgrammeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped(typeof(ISemesterService<>), typeof(SemesterService<>));
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IToastService, ToastService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredModal();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });

await builder.Build().RunAsync();
