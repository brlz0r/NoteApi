using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoteApi;
using NoteApi.Models.HttpException;
using NoteApi.Repositories;
using NoteApi.Repositories.Interfaces;
using NoteApi.Services;
using NoteApi.Services.Interfaces;
using NoteApi.Utils;
using NoteApi.Utils.interfaces;
using NoteApi.Utils.Interfaces;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
builder.Services.AddDbContext<AppDatabaseContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<ITokens, Tokens>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes("pCX02gaJHfPwdiovzkNjuREyTMjB2R")),
        ValidIssuer = "NoteAppServer",
        ValidAudience = "NoteAppUser",
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        switch (exceptionHandlerPathFeature?.Error)
        {
            case BadHttpRequestException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await context.Response.WriteAsJsonAsync(
                    new { error = exceptionHandlerPathFeature.Error.Message });
                break;
            case UnauthorizedException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                await context.Response.WriteAsJsonAsync(
                    new { error = exceptionHandlerPathFeature.Error.Message });
                break;
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await context.Response.WriteAsJsonAsync(
                    new { error = exceptionHandlerPathFeature.Error.Message });
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(
                    "Unhandled Error! Contact support");
                break;
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
