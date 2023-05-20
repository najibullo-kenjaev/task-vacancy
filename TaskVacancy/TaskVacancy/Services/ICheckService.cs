using TaskVacancy.Dto;

namespace TaskVacancy.Services
{
    public interface ICheckService
    {
        Task<bool> CheckAccount(CheckAccountDto checkAccount);
        Task<float> GetBalanceWallet(CheckAccountDto checkAccount);
    }
}
