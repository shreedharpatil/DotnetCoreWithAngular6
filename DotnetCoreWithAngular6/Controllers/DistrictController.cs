using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public DistrictController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("{stateId}")]
        public IActionResult Get(int stateId)
        {
            return this.Ok(this.addressRepository.GetDistricts(stateId));
        }

        [HttpPost]
        [Route("{stateId}")]
        public IActionResult Post(int stateId, [FromBody] District district)
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
