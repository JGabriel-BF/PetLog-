using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetLog.Aplicacao;
using PetLog.Repositorio;
using PetLog.Repositorio.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IVendedorAplicacao, VendedorAplicacao>();
builder.Services.AddScoped<IClienteAplicacao, ClienteAplicacao>();
builder.Services.AddScoped<IProdutoAplicacao, ProdutoAplicacao>();
builder.Services.AddScoped<ICompraAplicacao, CompraAplicacao>();

//interfaces do banco
builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<ICompraRepositorio, CompraRepositorio>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<PetLogContexto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(" PetLog")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();