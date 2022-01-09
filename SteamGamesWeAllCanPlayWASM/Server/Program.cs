using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using SteamGamesWeAllCanPlay.Helper;
using SteamGamesWeAllCanPlayWASM.Client.Services;
using SteamGamesWeAllCanPlayWASM.Data;
using SteamGamesWeAllCanPlayWASM.Data.Repositories;
using SteamGamesWeAllCanPlayWASM.Server.Helpers;
using SteamWebAPI2.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("ConnectionSqlite");
string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING_SQLITE");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddAuthentication(options => 
                { 
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
                })
                .AddCookie(options =>
                {
                    //options.Cookie.SameSite = SameSiteMode.Unspecified;
                    options.LoginPath = "/signin";
                    options.LogoutPath = "/signout";
                    options.AccessDeniedPath = "/";
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.Events.OnSignedIn = ValidationHelper.SignIn;
                    options.Events.OnValidatePrincipal = ValidationHelper.Validate;
                })
                .AddSteam(options =>
                {
                    options.CorrelationCookie.SameSite = SameSiteMode.None;
                    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
                });

string steamAPIKey = Environment.GetEnvironmentVariable("STEAM_API_KEY");

//Console.WriteLine("SteamKey:"+steamAPIKey);

builder.Services.AddTransient(x => new SteamWebInterfaceFactory(steamAPIKey));

builder.Services.AddDataProtection()
    .SetApplicationName("SteamGamesWeAllCanPlayWASM")
    .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"/var/dpkeys/"));



builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.CreateDatabase<ApplicationContext>();

app.Run();
