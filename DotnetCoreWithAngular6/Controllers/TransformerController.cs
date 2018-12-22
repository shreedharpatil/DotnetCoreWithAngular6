using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformerController : ControllerBase
    {
        private readonly ITransformerRepository transformerRepository;

        public TransformerController(ITransformerRepository transformerRepository)
        {
            this.transformerRepository = transformerRepository;
        }

        [HttpPost]
        [Route("{type}/{typeId}")]
        public IActionResult Post(string type, int typeId, [FromBody] Transformer transformer)
        {
            try
            {
                this.transformerRepository.AddTransformer(type, typeId, transformer);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
