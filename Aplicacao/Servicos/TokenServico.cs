
using Aplicacao.Interfaces;
using Dominio.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Aplicacao.Servicos;

public class TokenServico : ITokenServico
{
    private readonly IConfiguration _configuration;

    public TokenServico(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GerarToken(Usuario usuario)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"] ?? string.Empty));
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var credencial = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(type: ClaimTypes.Email, usuario.Email),
            new Claim(type: ClaimTypes.Role, usuario.Funcao.ToString()),
        };

        var tokenOpcoes = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credencial);

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOpcoes);
        return token;
    }
}