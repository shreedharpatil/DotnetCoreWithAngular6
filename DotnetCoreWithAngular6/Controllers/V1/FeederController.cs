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
    public class FeederV1Controller : ControllerBase
    {
        private readonly IFeederRepository feederRepository;

        public FeederV1Controller(IFeederRepository feederRepository)
        {
            this.feederRepository = feederRepository;
        }

        [HttpPost]
        [Route("{type}")]
        public IActionResult Post(string type, [FromBody] Feeder feeder)
        {
            try
            {
                this.feederRepository.AddFeeder(type, feeder);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
