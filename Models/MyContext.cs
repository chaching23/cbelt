using Microsoft.EntityFrameworkCore;
 
namespace belt.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions  options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Act> Activities {get; set;}
        public DbSet<Join> Participants {get; set;}


    }
}


