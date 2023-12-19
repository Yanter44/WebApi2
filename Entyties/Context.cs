using Microsoft.EntityFrameworkCore;

using PopastNaStajirovku2.Models;



namespace PopastNaStajirovku2.Entyties
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
     
        public Context()
        {
            
            Database.EnsureCreated();
            Database.OpenConnection();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-S9AIDDH\\SQLEXPRESS; Database=AspKnowledge; Trusted_Connection=True; TrustServerCertificate=True");
        }

    }
}
