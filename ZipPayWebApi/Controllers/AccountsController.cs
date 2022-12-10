using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZipPayWebApp.BAL;
using ZipPayWebApp.BAL.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZipPayWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        
        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            return Ok(_accountsService.GetAccount(userId));
        }

        
        [HttpPost("Create")]
        public IActionResult Post([FromBody] AccountModel account)
        {
            return Ok(_accountsService.CreateAccount(account));
        }

        [HttpGet]
        public IActionResult GetAll(int recordsPerPage, int pageNo)
        {
            return Ok(_accountsService.GetAll(recordsPerPage, pageNo));
        }

      
    }
}
