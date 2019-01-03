using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TalukV1Controller : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public TalukV1Controller(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpPost]
        [Route("{stateId}/{districtId}")]
        public IActionResult Post(int stateId, int districtId, [FromBody] Taluk taluk)
        {
            try
            {
                this.addressRepository.AddTaluk(stateId, districtId, taluk);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
