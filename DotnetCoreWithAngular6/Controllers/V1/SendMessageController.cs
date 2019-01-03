using Common.Layer.DTO;
using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class SendMessageV1Controller : ControllerBase
    {
        private readonly ISendMessage sendMessage;

        public SendMessageV1Controller(ISendMessage sendMessage)
        {
            this.sendMessage = sendMessage;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SendMessageDTO sendMessage)
        {
            try
            {
                var result = await this.sendMessage.Send(sendMessage);
                return this.Ok(new { MobileNumbers = result.Item2, Result = result.Item1 });
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
