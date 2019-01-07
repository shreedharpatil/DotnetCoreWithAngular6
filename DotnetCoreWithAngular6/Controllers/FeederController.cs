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
    public class FeederController : ControllerBase
    {
        private readonly IFeederRepository feederRepository;

        public FeederController(IFeederRepository feederRepository)
        {
            this.feederRepository = feederRepository;
        }

        [HttpPost]
        [Route("{type}/{typeId}")]
        public IActionResult Post(string type, int typeId, [FromBody] Feeder feeder)
        {
            try
            {
                this.feederRepository.AddFeeder(type, typeId, feeder);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
