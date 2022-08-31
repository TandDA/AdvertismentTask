using System.ComponentModel.DataAnnotations.Schema;

namespace AdvertismentTask.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; } = false;
        public string? Image { get; set; }
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";

        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
