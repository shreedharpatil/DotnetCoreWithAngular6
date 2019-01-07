using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Data.Repository.Models;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/village")]
    [ApiController]
    [Authorize]
    public class VillageV1Controller : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public VillageV1Controller(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpPost]
        [Route("{stateId}/{districtId}/{talukId}")]
        public async Task<IActionResult> Post(string stateId, string districtId, string talukId, [FromBody] Village village)
        {
            try
            {
                await this.addressRepository.AddVillage(stateId, districtId, talukId, village);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
