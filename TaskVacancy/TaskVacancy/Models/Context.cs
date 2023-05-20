using Microsoft.EntityFrameworkCore;

namespace TaskVacancy.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransactions> AccountTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        FirstName = "Najibulllo",
                        LastName = "Kenjaev",
                        Email = "najibullokenjaev@gmail.com",
                        Address = "Dushanbe",
                        Birthday = new DateTime(1998, 9, 16),
                        IsIdentified = true,
                        Phone = "937479909"
                    },
                    new User
                    {
                        Id = 2,
                        FirstName = "Muhammad",
                        LastName = "Ishanqulov",
                        Email = "ishanqulov@gmail.com",
                        Address = "Dushanbe",
                        Birthday = new DateTime(1996, 9, 16),
                        IsIdentified = false,
                        Phone = "900900900"
                    },
                    new User
                    {
                        Id = 3,
                        FirstName = "Aziz",
                        LastName = "shodiev",
                        Email = "shodiev@gmail.com",
                        Address = "Dushanbe",
                        Birthday = new DateTime(1998, 9, 16),
                        IsIdentified = false,
                        Phone = "918282868"
                    }
                );
            modelBuilder.Entity<Account>()
                .HasData(
                    new Account
                    {
                        Id = 1,
                        UserId = 1,
                        Balance = 0
                    },
                    new Account
                    {
                        Id = 2,
                        UserId = 2,
                        Balance = 0
                    }, new Account
                    {
                        Id = 3,
                        UserId = 3,
                        Balance = 0
                    }
                );

        }
    }
}

