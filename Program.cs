using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PopastNaStajirovku2.Entyties;
using Microsoft.OpenApi.Models;
using System.Collections.ObjectModel;
using WebApi2.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<JwtTokenValidator>();
builder.Services.AddDbContext<Context>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "YongeApi",
            ValidAudience = "users",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d4d202f4210bf8335095eeb822a24f0c"))
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
          new string[]{}
        }

    });
    
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MY API");
        
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
//app.Use(async (context, next) =>
//{
//    string token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//    if (string.IsNullOrEmpty(token))
//    {
//        context.Response.StatusCode = 401;
//        await context.Response.WriteAsync("Unauthorized. No token provided.");
//        return;
//    }

//    var tokenValidator = context.RequestServices.GetRequiredService<JwtTokenValidator>();
//    if (tokenValidator.ValidateToken(token))
//    {
//        await next.Invoke();
    
//    }
//    else
//    {
//        context.Response.StatusCode = 403;
//        await context.Response.WriteAsync("Forbidden. Token validation failed.");
//    }
//});


app.MapControllers();

app.Run();

