using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TransformerController : ControllerBase
    {
        private readonly ITransformerRepository transformerRepository;

        public TransformerController(ITransformerRepository transformerRepository)
        {
            this.transformerRepository = transformerRepository;
        }

        [HttpPost]
        [Route("{type}/{typeId}/{feederId}")]
        public IActionResult Post(string type, int typeId, int feederId, [FromBody] Transformer transformer)
        {
            try
            {
                this.transformerRepository.AddTransformer(type, typeId, feederId, transformer);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
