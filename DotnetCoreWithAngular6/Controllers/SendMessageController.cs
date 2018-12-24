using Common.Layer.DTO;
using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly ISendMessage sendMessage;

        public SendMessageController(ISendMessage sendMessage)
        {
            this.sendMessage = sendMessage;
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] SendMessageDTO sendMessage)
        {
            try
            {
                this.sendMessage.Send(sendMessage);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500);
            }
        }
    }
}
