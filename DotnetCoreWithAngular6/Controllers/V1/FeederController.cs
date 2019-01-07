using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Data.Repository.Models;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/feeder")]
    [ApiController]
    [Authorize]
    public class FeederV1Controller : ControllerBase
    {
        private readonly IFeederRepository feederRepository;

        public FeederV1Controller(IFeederRepository feederRepository)
        {
            this.feederRepository = feederRepository;
        }

        [HttpPost]
        [Route("{type}/{typeId}")]
        public async Task<IActionResult> Post(string type, string typeId, [FromBody] Feeder feeder)
        {
            try
            {
                await this.feederRepository.AddFeeder(type, typeId, feeder);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
