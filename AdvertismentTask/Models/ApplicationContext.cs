using Microsoft.EntityFrameworkCore;

namespace AdvertismentTask.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Advertisement> Advertisements { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(b => b.Name).HasMaxLength(30);
            modelBuilder.Entity<User>().Property(b => b.Password).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(b => b.Email).HasMaxLength(30);

            modelBuilder.Entity<User>().HasData(

                new User { Id = 1, Name = "Admin", Email = "test@yandex.ru", Password = "123" },
                new User { Id = 2, Name = "Sam", Email = "test@yandex.ru", Password = "123" },
                new User { Id = 3, Name = "Bob", Email = "test@yandex.ru", Password = "123" }
            );
            modelBuilder.Entity<Advertisement>().HasData(
                new Advertisement { Id = 1, Title="Заголовок", Text = "Какой-то текст", CreationDate = DateTime.Now, IsAvailable = true, Image= @"/images/standartImage.jpg", UserId = 2 },
                new Advertisement { Id = 2, Title = "Заголовок 2", Text = "Какой-то текст", CreationDate = DateTime.Now, IsAvailable = false, Image = @"/images/standartImage.jpg", UserId = 2 },
                new Advertisement { Id = 3, Title = "Заголовок 3", Text = "Какой-то текст", CreationDate = DateTime.Now, IsAvailable = false, Image = @"/images/standartImage.jpg", UserId = 3 },
                new Advertisement { Id = 4, Title = "Заголовок 4", Text = "Какой-то текст", CreationDate = DateTime.Now, IsAvailable = false, Image = @"/images/standartImage.jpg", UserId = 3 }
            );

        }
    }
}
