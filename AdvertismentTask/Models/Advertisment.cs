namespace AdvertismentTask.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Title { get; set; } = null!;
        public string? Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
