﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Layer.Models;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Login login)
        {
            try
            {
                return this.Ok(this.loginRepository.ValidateLoginCredentials(login));
            }
            catch(Exception ex)
            {
                return this.Ok(ex);
            }            
        }
    }
}