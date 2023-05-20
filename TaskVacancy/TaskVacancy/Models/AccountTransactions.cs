namespace TaskVacancy.Models
{
    public class AccountTransactions
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public float Amount { get; set; }
        public float Before { get; set; }
        public float After { get; set; }
        public string Commnet { get; set; }
    }
}
