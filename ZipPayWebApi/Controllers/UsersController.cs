using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZipPay.BAL;
using ZipPay.BAL.Entity;

namespace ZipPayWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_usersService.GetAllUser());
        }

       
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_usersService.GetUser(id));
        }

        
        [HttpPost("Create")]
        public IActionResult Post([FromBody] UserModel user)
        {
            return Ok(_usersService.CreateUser(user));
        }

       
    }
}
