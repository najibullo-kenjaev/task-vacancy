using TaskVacancy.Dto;

namespace TaskVacancy.Services
{
    public interface ITransactionService
    {
        Task<ReceiptReplenishmentViewDto> ReplenishmentBalance(ReplenishmentBalanceDto replenishmentBalance);
        Task<AccountTransactionDto> GetTotalAmount();
    }
}
