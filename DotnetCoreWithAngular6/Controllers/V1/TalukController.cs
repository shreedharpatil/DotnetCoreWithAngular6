
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Data.Repository.Models;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/taluk")]
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
        public IActionResult Post(string stateId, string districtId, [FromBody] Taluk taluk)
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
