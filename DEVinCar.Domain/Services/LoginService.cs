using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.Exceptions;

namespace DEVinCar.Domain.Services
{
    public class LoginService : ILoginService
    
    {

        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public User CheckUser(LoginDTO loginDTO)
        {
           var incomingUser = _loginRepository.CheckUser(loginDTO);

           return(incomingUser);
        }

    }
}