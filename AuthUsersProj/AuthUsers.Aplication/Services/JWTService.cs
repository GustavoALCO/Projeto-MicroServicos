using AuthUsers.Aplication.Interfaces;
using AuthUsers.Aplication.Settings;
using AuthUsers.domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace AuthUsers.Aplication.Services;

public class JWTService : IJWTService
{
    private readonly JWTSettings _configuration;

    public JWTService(IOptions<JWTSettings> configuration)
    {
        _configuration = configuration.Value;
        //configuração para acessar informaçoes do appsetings
    }
    public string GerarTokenEmployee(string email, string position)
    {
        var chaveScreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
        //busca a chave secreta que esta no appsetings 

        var credentials = new SigningCredentials(chaveScreta, SecurityAlgorithms.HmacSha256);
        //informa a chave o tipo de segurança para a criação do Header do JWT

        var claims = new[]
        {
                new Claim("login", email),
                new Claim("Role", position)
            };

        var token = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience[0],
                //Audience e o Issuer são assinaturas para que o Jwt funcione corretamente
                claims: claims,
                //claims serve para passar dados adicionais do gerador do código
                expires: DateTime.Now.AddHours(Convert.ToInt16(_configuration.ExpireHours)),
                //Define quantas horas o token vai existir
                signingCredentials: credentials
                );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GerarTokenUser(string email, string name, string surname)
    {
        var chaveScreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
        //busca a chave secreta que esta no appsetings 

        var credentials = new SigningCredentials(chaveScreta, SecurityAlgorithms.HmacSha256);
        //informa a chave o tipo de segurança para a criação do Header do JWT

        var claims = new[]
        {
                new Claim("login", email),
                new Claim("nome", name),
                new Claim("sobrenome", surname),
            };

        var token = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience[0],
                //Audience e o Issuer são assinaturas para que o Jwt funcione corretamente
                claims: claims,
                //claims serve para passar dados adicionais do gerador do código
                expires: DateTime.Now.AddHours(Convert.ToInt16(_configuration.ExpireHours)),
                //Define quantas horas o token vai existir
                signingCredentials: credentials
                );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
