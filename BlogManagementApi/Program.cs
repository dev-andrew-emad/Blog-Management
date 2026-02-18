using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;
using blogdatalayer.data;
using blogbusinesslayer.business;
using blogdatalayer.dbcontext;
using blogbusinesslayer.dtos;

var builder = WebApplication.CreateBuilder(args);

string constr = builder.Configuration.GetConnectionString("connstr");

builder.Services.AddDbContext<appdbcontext>(options =>
options.UseSqlServer(constr));
builder.Services.AddScoped<userdata>();
builder.Services.AddScoped<userbusiness>();
builder.Services.AddScoped<postdata>();
builder.Services.AddScoped<postbusiness>();
builder.Services.AddScoped<commentdata>();
builder.Services.AddScoped<commentbusiness>();
builder.Services.AddScoped<likedata>();
builder.Services.AddScoped<likebusiness>();
builder.Services.AddScoped<commentlikedata>();
builder.Services.AddScoped<commentlikebusiness>();
builder.Services.AddScoped<replydata>();
builder.Services.AddScoped<replybusiness>();
builder.Services.AddScoped<replylikedata>();
builder.Services.AddScoped<replylikebusiness>();


builder.Services.Configure<jwtsetting>(builder.Configuration.GetSection("jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var jwtsetting = builder.Configuration.GetSection("jwt");
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtsetting["Issuer"],
            ValidAudience = jwtsetting["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting["Key"]))
        };
    });
    
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer {your token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,Id="Bearer"
                }
            },
            new string []{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
