using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DistrictV1Controller : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public DistrictV1Controller(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("{stateId}")]
        public IActionResult Get(string stateId)
        {
            return this.Ok(this.addressRepository.GetDistricts(stateId));
        }

        [HttpPost]
        [Route("{stateId}")]
        public IActionResult Post(string stateId, [FromBody] District district)
        {
            try
            {
                this.addressRepository.AddDistrict(stateId, district);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
