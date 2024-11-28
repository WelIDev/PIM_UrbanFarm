using System.Text;
using System.Text.Json.Serialization;
using Aplicacao.Interfaces;
using Aplicacao.Servicos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorios;
using Dominio.Interfaces.Servicos;
using Infraestrutura.Persistencia;
using Infraestrutura.Persistencia.DbInitializer;
using Infraestrutura.Persistencia.Repositorios;
using Infraestrutura.Servicos;
using Infraestrutura.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "PIM_UrbanFarmAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor insira o token JWT no formato: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
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
            new string[] { }
        }
    });
});

// Configuração da conexão com o banco de dados
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration
    .GetConnectionString("Default")));

// Configuração de injeção de dependência
// Repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IFornecedorRepositorio, FornecedorRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();
builder.Services.AddScoped<IVendaRepositorio, VendaRepositorio>();
builder.Services.AddScoped<IHistoricoCompraRepositorio, HistoricoCompraRepositorio>();
builder.Services.AddScoped<IAbastecimentoEstoqueRepositorio, AbastecimentoEstoqueRepositorio>();

// Servicos
builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
builder.Services.AddScoped<ISegurancaServico, SegurancaServico>();
builder.Services.AddScoped<ITokenServico, TokenServico>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
builder.Services.AddHttpClient<ICepServico, CepServico>();
builder.Services.AddScoped<IConsultarCepServico, ConsultarCepServico>();
builder.Services.AddScoped<IClienteServico, ClienteServico>();
builder.Services.AddScoped<IFornecedorServico, FornecedorServico>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
builder.Services.AddScoped<IVendedorServico, VendedorServico>();
builder.Services.AddScoped<IVendaServico, VendaServico>();
builder.Services.AddScoped<IHistoricoCompraServico, HistoricoCompraServico>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAbastecimentoEstoqueServico, AbastecimentoEstoqueServico>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Configuração do JWT
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"] ??
                                           string.Empty)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configura o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Urls.Add("https://*:7124");

// Executa a atualização do banco de dados(migrations)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    DbInitializer.Seed(dbContext);
}

app.Run();
