namespace TaskVacancy.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public float Balance { get; set; }
    }
}
