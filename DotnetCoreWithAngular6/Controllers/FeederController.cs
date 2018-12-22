using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeederController : ControllerBase
    {
        private readonly IFeederRepository feederRepository;

        public FeederController(IFeederRepository feederRepository)
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
