var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>{
    options.AddPolicy("ReactPolicy", policy =>{
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();}
        );
    }
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("ReactPolicy");
app.MapControllers();

app.Run();