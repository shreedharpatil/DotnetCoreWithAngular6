using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalukController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public TalukController(IAddressRepository addressRepository)
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
