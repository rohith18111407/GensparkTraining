using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterApplication.Models
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<TweetHashtag> TweetHashtags { get; set; } = new List<TweetHashtag>();
    }
}
