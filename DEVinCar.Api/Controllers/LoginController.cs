using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DEVinCar.Domain.DTOs;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace DEVinCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login ([FromBody] LoginDTO loginDTO)
        {
            var incomingUser = _loginService.CheckUser(loginDTO);
            
            if (incomingUser == null)
            {
                return Unauthorized();
            }

            // var token = TokenService.GenerateTokenFromUser(incomingUser);
            // var refreshToken = TokenService.GenerateRefreshToken();
            // TokenService.SaveRefreshToken(incomingUser.Name, refreshToken);

            // return Ok(new {
            //     token,
            //     refreshToken
            // });

            return Ok();
        }
        
        // [HttpPost]
        // [Route("refresh")]
        // [AllowAnonymous]
        // public IActionResult RefreshToken([FromQuery] string token, [FromQuery] string refreshToken)
        // {
        //     var principal = TokenService.GetPrincipalFromExpiredToken(token);
        //     var username = principal.Identity.Name;
        //     var savedRefreshToken = TokenService.GetRefreshToken(username);

        //     if (savedRefreshToken != refreshToken)
        //         throw new SecurityTokenException("Invalid refresh token");

        //     var newToken = TokenService.GenerateTokenFromClaims(principal.Claims);
        //     var newRefreshToken = TokenService.GenerateRefreshToken();
        //     TokenService.DeleteRefreshToken(username, refreshToken);
        //     TokenService.SaveRefreshToken(username, newRefreshToken);

        //     return new ObjectResult(new
        //     {
        //         token = newToken,
        //         refreshToken = newRefreshToken
                
        //     });

        // }

        // [HttpGet]
        // [Route("list-refresh-tokens")]
        // [AllowAnonymous]
        // public IActionResult ListRefreshTokens(){
        //     return Ok(TokenService.GetAllRefreshTokens());
        // }
    }
}