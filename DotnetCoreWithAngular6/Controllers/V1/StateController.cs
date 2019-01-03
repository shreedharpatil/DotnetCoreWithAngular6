using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StateV1Controller : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public StateV1Controller(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.addressRepository.GetStates());
        }

        [HttpPost]
        public IActionResult Post([FromBody] State state)
        {
            try
            {
                this.addressRepository.AddState(state);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
