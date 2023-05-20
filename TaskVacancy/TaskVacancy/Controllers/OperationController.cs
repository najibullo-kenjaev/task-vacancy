using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskVacancy.Dto;
using TaskVacancy.Services;

namespace TaskVacancy.Controllers
{
    [Route("api/operations")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly ICheckService checkService;
        public OperationController(ITransactionService transactionService, ICheckService checkService)
        {
            this.transactionService = transactionService;
            this.checkService = checkService;
        }

        [HttpPost("check-account")]
        public async Task<IActionResult> CheckAccount([FromBody] CheckAccountDto entityDto)
        {
            var result = await checkService.CheckAccount(entityDto);
            return Ok(result);
        }

        [HttpPost("check-balance")]
        public async Task<IActionResult> GetBalance([FromBody] CheckAccountDto entityDto)
        {
            var result = await checkService.GetBalanceWallet(entityDto);
            return Ok(result);
        }


        [HttpPost("replenishment-balance")]
        public async Task<IActionResult> ReplenishmentBalance([FromBody] ReplenishmentBalanceDto entityDto)
        {
            var result = await transactionService.ReplenishmentBalance(entityDto);
            return Ok(result);
        }

        [HttpPost("get-total-amount")]
        public async Task<IActionResult> GetTotalAmount([FromBody] CheckAccountDto entityDto)
        {
            var result = await transactionService.GetTotalAmount();
            return Ok(result);
        }

    }
}
