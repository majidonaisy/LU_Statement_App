using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using StatementApplication.AuthorizationRequirements;
using StatementApplication.Configs;
using StatementApplication.Data;
using StatementApplication.Handler;
using StatementApplication.Models;
using StatementApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Specify the login page here
        
    });
builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("VerifiedStudent", policy =>
        policy.Requirements.Add(new VerifiedStudentRequirement()));
});


builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<EmailSender,EmailSender>();

builder.Services.AddScoped<EmailVerificationTokenMaker,EmailVerificationTokenMaker>();
builder.Services.AddTransient<IAuthorizationHandler, VerifyStudentHandler>();

var _secretKey = builder.Configuration["SecretKey"];
builder.Services.AddSingleton<SecretKey>(new SecretKey
{
    secretKey = _secretKey!
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();
app.MapRazorPages();

app.Run();
