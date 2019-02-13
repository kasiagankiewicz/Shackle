using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shackle.Services.Accounts;
using Shackle.Services.Blockchain;
using Shackle.Services.Dto;
using Shackle.Services.Requests;

namespace Shackle.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockchainService _blockchainService;

        public BlockchainController(IBlockchainService blockchainService)
        {
            _blockchainService = blockchainService;
        }

        [HttpGet("blockchain/blocks/{index}")]
        public ActionResult<BlockDto> GetBlock(int index)
        {
            var block = _blockchainService.GetBlock(index);
            if (block is null)
            {
                return NotFound();
            }

            return new BlockDto(block);
        }
        
        [HttpGet("blockchain")]
        public ActionResult<BlockchainDto> GetBlockchain()
        {
            return new BlockchainDto(_blockchainService.Blockchain);
        }
        
        [HttpGet("blockchain/blocks/last")]
        public ActionResult<BlockDto> GetLastBlock()
        {
            var block = _blockchainService.GetLastBlock();
            if (block is null)
            {
                return NotFound();
            }

            return new BlockDto(block);
        }
        
        [HttpGet("blockchain/difficulty")]
        public ActionResult<int> GetDifficulty()
        {
            return _blockchainService.GetDifficulty();
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
            _blockchainService.CreateTransaction(sender, receiver, request.Amount);

            return Ok();
        }

        [HttpPut("blockchain/difficulty/{difficulty}")]
        public ActionResult Put(int difficulty)
        {
            if (difficulty <= 0)
            {
                return NotFound();
            }
            
            _blockchainService.SetDifficulty(difficulty);
            return Ok();
        }
    }
}