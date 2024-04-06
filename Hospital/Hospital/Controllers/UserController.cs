using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Hospital.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    [HttpGet("Admins")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminsEndpoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi {currentUser.Username}");
    }


    [HttpGet("Users")]
    [Authorize(Roles = "User")]
    public IActionResult UsersEndpoint()
    {
        var currentUser = GetCurrentUser();

        return Ok($"Hi {currentUser.Username}");
    }

    [HttpGet("AdminsAndSellers")]
    [Authorize(Roles = "Administrator,Seller")]
    public IActionResult AdminsAndSellersEndpoint()
    {
        var currentUser = GetCurrentUser();

        return Ok($"Hi {currentUser.Username}, you are an {currentUser.Roles}");
    }

    [HttpGet("Public")]
    public IActionResult Public()
    {
        return Ok("Hi, you're on public property");
    }

    private User GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new User
            {
                UserId = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value),
                Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
            };
        }

        return null;
    }
}