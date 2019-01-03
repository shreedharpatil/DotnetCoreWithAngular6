using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreWithAngular6.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserV1Controller : ControllerBase
    {
        private readonly IUserRepository userRepository;

        private readonly Data.Repository.Interfaces.IUserRepository userRepository1;

        public UserV1Controller(IUserRepository userRepository, Data.Repository.Interfaces.IUserRepository userRepository1)
        {
            this.userRepository = userRepository;
            this.userRepository1 = userRepository1;
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

        [HttpGet]
        [Route("get")]
        public IActionResult Get1()
        {
            return this.Ok(this.userRepository1.GetUsers());
        }

        [HttpPost]
        [Route("post")]
        public IActionResult Post1([FromBody] Data.Repository.Models.User user)
        {
            this.userRepository1.AddUser(user);
            return this.Ok();
        }
    }
}