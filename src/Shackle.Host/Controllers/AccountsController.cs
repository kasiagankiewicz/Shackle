using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shackle.Services.Accounts;
using Shackle.Services.Dto;
using Shackle.Services.Requests;

namespace Shackle.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<AccountDto>> Get()
        {
            return Ok(_accountService.GetAll().Select(a => new AccountDto(a)));
        }

        [HttpGet("{name}")]
        public ActionResult<AccountDetailsDto> Get(string name)
        {
            var account = _accountService.Get(name);
            if (account is null)
            {
                return NotFound();
            }

            return new AccountDetailsDto(account);
        }
        
        [HttpPost]
        public ActionResult Post(Join request)
        {
            _accountService.Join(request.Name, request.Balance ?? 100);
            return CreatedAtAction(nameof(Get), new {name = request.Name}, null);
        }
    }
}