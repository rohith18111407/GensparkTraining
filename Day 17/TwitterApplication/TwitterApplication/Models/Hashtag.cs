using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterApplication.Models
{
    public class Hashtag
    {
        [Key]
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;

        public ICollection<TweetHashtag> TweetHashtags { get; set; } = new List<TweetHashtag>();

    }
}
