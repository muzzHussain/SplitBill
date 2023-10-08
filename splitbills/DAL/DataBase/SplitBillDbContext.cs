using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataBase
{
    public class SplitBillDbContext: IdentityDbContext<IdentityUser>
    {
        private string _dbConnectionString;

        public SplitBillDbContext()
        {
        }

        public SplitBillDbContext(DbContextOptions<SplitBillDbContext> options):base(options)
        {
           
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.;Database=WalletDb;Trusted_Connection=True");
        //}


        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupExpense> GroupExpenses { get; set; }
        public DbSet<UserExpense> UserExpenses { get; set; }
        public DbSet<UsersGroup> UsersGroups { get; set; }
    }
}
