using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetById(long userId)
        {
            var user = _userService.GetById(userId);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel userModel)
        {
            var id = await _userService.Create(userModel);

            return Ok(id);
        }
    }
}
