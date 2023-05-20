using Microsoft.EntityFrameworkCore;
using TaskVacancy.Dto;
using TaskVacancy.Exceptions;
using TaskVacancy.Models;

namespace TaskVacancy.Services.Implementations
{
    public class CheckService : ICheckService
    {
        private readonly Context context;
        public CheckService(Context context)
        {
            this.context = context;
        }
        public async Task<bool> CheckAccount(CheckAccountDto checkAccount)
        {
            var userAccount = await context.Users.AnyAsync(x => x.Phone == checkAccount.PhoneNumber);
            if (userAccount == null)
                throw new WalletException(WalletErrors.ACCOUNT_NOT_FOUND);
            return userAccount;
        }

        public async Task<float> GetBalanceWallet(CheckAccountDto checkAccount)
        {
            var balance = await context.Accounts
                .Include(u => u.User)
                .FirstOrDefaultAsync(x => x.User.Phone == checkAccount.PhoneNumber);
            if (balance == null)
                throw new WalletException(WalletErrors.ACCOUNT_NOT_FOUND);
            return balance.Balance;
        }
    }
}
