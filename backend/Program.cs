using System.Text;
using backend.Data;
using backend.services;
using backend.services.authservice;
using backend.services.products;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>{
    options.AddPolicy("ReactPolicy", policy =>{
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();}
        );
    }
);

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>{
        options.TokenValidationParameters = new(){
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    }
);


builder.Services.AddControllers();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<JwtService>();


//pós builder
var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.UseCors("ReactPolicy");
app.MapControllers();

app.Run();