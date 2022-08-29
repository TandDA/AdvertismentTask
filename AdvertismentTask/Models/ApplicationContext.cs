using Microsoft.EntityFrameworkCore;

namespace AdvertismentTask.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Advertisement> Advertisements { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {    
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(b => b.Name).HasMaxLength(30);
            modelBuilder.Entity<User>().Property(b => b.Password).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(b => b.Email).HasMaxLength(30);

        }
    }
}
