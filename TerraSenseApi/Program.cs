using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using TerraSenseApi.Data;

var builder = WebApplication.CreateBuilder(args);

// EF Core + Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"))
);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TerraSense Reports API",
        Version = "v1",
        Description = "API REST para gerenciamento de relatórios ambientais e observações das plantações do sistema TerraSense."
    });
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();