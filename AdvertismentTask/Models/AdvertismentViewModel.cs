namespace AdvertismentTask.Models
{
    public class AdvertismentViewModel
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
}
