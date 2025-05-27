namespace TwitterApplication.Models
{
    public class UserFollow
    {
        public int FollowerId { get; set; }
        public User Follower { get; set; } = null!;

        public int FolloweeId { get; set; }
        public User Followee { get; set; } = null!;
    }
}
