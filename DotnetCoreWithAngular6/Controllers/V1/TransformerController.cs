using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Data.Repository.Models;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/transformer")]
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
        [Route("{type}/{typeId}/{feederId}")]
        public async Task<IActionResult> Post(string type, string typeId, string feederId, [FromBody] Transformer transformer)
        {
            try
            {
               await this.transformerRepository.AddTransformer(type, typeId, feederId, transformer);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
