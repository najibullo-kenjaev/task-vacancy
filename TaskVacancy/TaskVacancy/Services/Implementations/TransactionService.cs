using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskVacancy.Dto;
using TaskVacancy.Exceptions;
using TaskVacancy.Models;

namespace TaskVacancy.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly Context context;
        private readonly IMapper mapper;
        public TransactionService(IMapper mapper, Context context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<AccountTransactionDto> GetTotalAmount()
        {
            var fromDate = DateTime.Now.AddDays(-30);
            var toDate = DateTime.Now;

            var accountCount = await context.AccountTransactions
                .Include(a => a.Account)
                .Where(x => (x.Date >= fromDate && x.Date <= toDate) &&
                x.Account.UserId == 2)
                .GroupBy(g => g.AccountId)
                .Select(a => new AccountTransactionDto
                {
                    Count = a.Count(),
                    TotalAmount = a.Sum(x => x.Amount),
                }).FirstAsync();
            return accountCount;
        }

        public async Task<ReceiptReplenishmentViewDto> ReplenishmentBalance(ReplenishmentBalanceDto replenishmentBalance)
        {
            var userAccount = await context.Accounts
                .Include(u => u.User)
                .FirstOrDefaultAsync(x => x.User.Phone == replenishmentBalance.Phone);

            if (userAccount == null)
                throw new WalletException(WalletErrors.ACCOUNT_NOT_FOUND);

            var IsIdentified = userAccount.User.IsIdentified;
            var balanceIsIdentified = userAccount.Balance + replenishmentBalance.Amount;
            if ((IsIdentified && balanceIsIdentified <= 100000) || balanceIsIdentified <= 10000)
            {
                AccountTransactions accountTransactions = new AccountTransactions();
                accountTransactions.AccountId = userAccount.Id;
                accountTransactions.Date = DateTime.Now;
                accountTransactions.TransactionType = (TransactionType)replenishmentBalance.TypeTransaction;
                accountTransactions.Amount = replenishmentBalance.Amount;
                accountTransactions.Before = userAccount.Balance;
                accountTransactions.After = userAccount.Balance + replenishmentBalance.Amount;
                accountTransactions.Commnet = replenishmentBalance.Comment;
                await context.AddAsync(accountTransactions);
                await context.SaveChangesAsync();

                userAccount.Balance = userAccount.Balance + replenishmentBalance.Amount;
                context.Entry(userAccount).State = EntityState.Modified;
                await context.SaveChangesAsync();
                context.Entry(accountTransactions).Reference(a => a.Account).Load();
                return mapper.Map<ReceiptReplenishmentViewDto>(accountTransactions);
            }
            else
            {
                throw new WalletException(WalletErrors.THE_AMOUNT_EXCEEDS_THE_LIMIT);
            }
        }

    }
}
