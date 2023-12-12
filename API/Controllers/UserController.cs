using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        Users user;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            user = new Users();
        }

        [HttpGet("users")]
        public async Task<IEnumerable<Users>> Get()
        {
            return await _userRepository.Get();
        }

        [HttpGet("user/{id}")]
        public async Task<IEnumerable<Users>> GetById(int id)
        {
            return await _userRepository.GetID(id);
        }

        [HttpPost("post")]
        public async Task<ActionResult<int>> Post([FromBody] Users user)
        {
            return await _userRepository.Create(user);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] Users user)
        {
            return await _userRepository.Update(id, user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }
    }
}
