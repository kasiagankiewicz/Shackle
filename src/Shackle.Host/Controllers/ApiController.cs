using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shackle.Core.Dto;
using Shackle.Core.Requests;
using Shackle.Core.Services;
using Shackle.Host.Requests;

namespace Shackle.Host.Controllers
{
    [Route("")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IBlockchainRunner _blockchainRunner;

        public ApiController(IAccountService accountService, IBlockchainRunner blockchainRunner)
        {
            _accountService = accountService;
            _blockchainRunner = blockchainRunner;
        }

        [HttpGet("accounts")]
        public ActionResult<IEnumerable<AccountDto>> Get()
        {
            return Ok(_accountService.GetAll().Select(a => new AccountDto(a)));
        }

        [HttpGet("accounts/{name}")]
        public ActionResult<AccountDetailsDto> Get(string name)
        {
            var account = _accountService.Get(name);
            if (account is null)
            {
                return NotFound();
            }

            return new AccountDetailsDto(account);
        }
        
        [HttpGet("blockchain")]
        public ActionResult<BlockchainDto> GetBlockchain()
        {
            return new BlockchainDto(_blockchainRunner.Blockchain);
        }

        [HttpPost("accounts")]
        public ActionResult Post(CreateAccount request)
        {
            _accountService.Create(request.Name, request.Balance ?? 100);
            return CreatedAtAction(nameof(Get), new {name = request.Name}, null);
        }

        [HttpPost("transfers")]
        public ActionResult Post(CreateTransfer request)
        {
            if (request.Sender is null)
            {
                return NotFound();
            }

            if (request.Receiver is null)
            {
                return NotFound();
            }

            if (request.Amount <= 0)
            {
                return NotFound();
            }

            var sender = _accountService.Get(request.Sender);
            var receiver = _accountService.Get(request.Receiver);
            _blockchainRunner.CreateTransfer(sender, receiver, request.Amount);

            return Ok();
        }
    }
}