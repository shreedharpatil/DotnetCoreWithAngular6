using Common.Layer.Models;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
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