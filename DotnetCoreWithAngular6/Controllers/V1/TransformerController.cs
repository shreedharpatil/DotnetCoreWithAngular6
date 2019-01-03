using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class TransformerV1Controller : ControllerBase
    {
        private readonly ITransformerRepository transformerRepository;

        public TransformerV1Controller(ITransformerRepository transformerRepository)
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
