using System.Text;
using DotNetEnv;
using backend.Data;
using backend.services;
using backend.services.authservice;
using backend.services.equipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using backend.Services.equipTypes;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key");
var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer");
var jwtAudience = Environment.GetEnvironmentVariable("Jwt__Audience");

builder.Services.AddDbContext<AppDbContext>(options =>options.UseNpgsql(connectionString));

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>{
        options.TokenValidationParameters = new(){
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
        };
    }
);

builder.Services.AddSwaggerGen(options =>{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Token JWT"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement{[new OpenApiSecuritySchemeReference("Bearer", document)] = []});
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>{options.AddPolicy("ReactPolicy", policy =>{policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();});});
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EquipTypeService>();
builder.Services.AddScoped<EquipmentService>();
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("ReactPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();