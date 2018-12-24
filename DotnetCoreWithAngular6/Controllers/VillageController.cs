using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VillageController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public VillageController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpPost]
        [Route("{stateId}/{districtId}/{talukId}")]
        public IActionResult Post(int stateId, int districtId, int talukId, [FromBody] Village village)
        {
            try
            {
                this.addressRepository.AddVillage(stateId, districtId, talukId, village);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
