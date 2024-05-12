using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Hospital.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class LoginController :  ControllerBase
{
    private IConfiguration _config;
    private readonly HospitalContext _dbContext;
    
    public LoginController(IConfiguration config, HospitalContext dbContext)
    {
        _config = config;
        _dbContext = dbContext;

    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
       

        var user = Authenticate(userLogin);
        Console.WriteLine(user);
        if (user != null)
        {
            var token = Generate(user);
            
            return Ok(token);
        }

        return NotFound("User not found");
    }

    private string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        Console.WriteLine(user.Roles);
        var rolesString = user.Roles.First().RoleName; 

        var claimsList = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username), 
            new Claim(ClaimTypes.Role, rolesString ),
        };
        var claims =claimsList.ToArray();

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User Authenticate(UserLogin userLogin)
    {
        Console.WriteLine(userLogin);
        var currentUser = _dbContext.Users
            .Include(u => u.Roles)   
            .FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

        if (currentUser != null)
        {
            return currentUser;
        }

        return null;
    }
}
    
