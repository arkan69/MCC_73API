using API.Contexts;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Dependencies Injection
builder.Services.AddScoped<UniversityRepositories>();
builder.Services.AddScoped<EmployeeRepositories>();
builder.Services.AddScoped<AccountRepositories>();
builder.Services.AddScoped<AccountRoleRepositories>();
builder.Services.AddScoped<RoleRepositories>();
builder.Services.AddScoped<ProfilingRepositories>();
builder.Services.AddScoped<EducationRepositories>();


// Configure SQL Server Databases
builder.Services.AddDbContext<MyContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

//Configure JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            //Usually, this is your application base URL
            //ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateAudience = false,
            //if the jwt are created using a web service, then this would be the consumer url
            //ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//Swagger UI
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Demo API", 
        Version = "v1", 
        Description="Detail Data Employee" 
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => 
        {
            policy.AllowAnyOrigin();//jika sudah ada website maka ganti allowany dengan with
            policy.AllowAnyHeader();//jika sudah ada website ganti allowany dengan with
            policy.AllowAnyMethod();
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

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
