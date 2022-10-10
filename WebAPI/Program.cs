using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Servicos;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(op =>
{
    op.AddPolicy("portal_usuario_angular", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200"
        ).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<SqlServerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IUsuario, RepositorioUsuario>();
builder.Services.AddSingleton<IServicoUsuario, ServicoUsuario>();
builder.Services.AddSingleton<IAplicacaoUsuario, AplicacaoUsuario>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    op =>
    {
        op.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Portal Usuario", Version = "v1" });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("portal_usuario_angular");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
