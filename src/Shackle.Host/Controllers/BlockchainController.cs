using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("blocks/{index}")]
        public ActionResult<BlockDto> GetBlock(int index)
        {
            var block = _blockchainService.GetBlock(index);
            if (block is null)
            {
                return NotFound();
            }

            return block;
        }

        [HttpGet()]
        public ActionResult<BlockchainDto> GetBlockchain()
        {
            var blockchain = _blockchainService.GetBlockchain();
            if (blockchain is null)
            {
                return NotFound();
            }

            return blockchain;
        }

        [HttpGet("blocks/current")]
        public ActionResult<BlockDto> GetCurrentBlock()
        {
            var block = _blockchainService.GetCurrentBlock();
            if (block is null)
            {
                return NotFound();
            }

            return block;
        }

        [HttpGet("difficulty")]
        public ActionResult<int> GetDifficulty() => _blockchainService.GetDifficulty();

        [HttpPost("transactions")]
        public ActionResult Post(CreateTransaction request)
        {
            _blockchainService.CreateTransaction(request.Sender, request.Receiver, request.Amount);

            return NoContent();
        }

        [HttpPut("difficulty/{difficulty}")]
        public ActionResult Put(int difficulty)
        {
            _blockchainService.SetDifficulty(difficulty);

            return NoContent();
        }
    }
}