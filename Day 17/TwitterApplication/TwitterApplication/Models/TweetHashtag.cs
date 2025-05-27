using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterApplication.Models
{
    public class TweetHashtag
    {
        [ForeignKey("Tweet")]
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; } = null!;
        [ForeignKey("Hashtag")]
        public int HashtagId { get; set; }
        public Hashtag Hashtag { get; set; } = null!;
    }
}
