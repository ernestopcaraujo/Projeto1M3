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
using DEVinCar.Domain.Models;

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
         public IActionResult Login ([FromBody] LoginDTO loginDTO)
        {
            
            var incomingUser = _loginService.CheckUser(loginDTO);
            
            if (incomingUser == null)
            {
                return Unauthorized();
            }
            var token = TokenService.GenerateTokenFromUser(incomingUser);
            return Ok(token);
        }
    }
}