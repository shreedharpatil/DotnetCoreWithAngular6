using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/user")]
    [ApiController]
    [Authorize]
    public class UserV1Controller : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserV1Controller(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.userRepository.GetUsers());
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            this.userRepository.AddUser(user);
            return this.Ok();
        }
    }
}