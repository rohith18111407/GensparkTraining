using Microsoft.EntityFrameworkCore;
using TwitterApplication.Models;

namespace TwitterApplication.Contexts
{
    public class TwitterContext : DbContext
    {
        public TwitterContext(DbContextOptions<TwitterContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Tweet> Tweets => Set<Tweet>();
        public DbSet<Like> Likes => Set<Like>();
        public DbSet<Hashtag> Hashtags => Set<Hashtag>();
        public DbSet<TweetHashtag> TweetHashtags => Set<TweetHashtag>();
        public DbSet<UserFollow> UserFollows => Set<UserFollow>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite keys
            modelBuilder.Entity<Like>().HasKey(l => new { l.UserId, l.TweetId });
            modelBuilder.Entity<TweetHashtag>().HasKey(th => new { th.TweetId, th.HashtagId });
            modelBuilder.Entity<UserFollow>().HasKey(f => new { f.FollowerId, f.FolloweeId });

            // Avoid circular cascade deletes
            modelBuilder.Entity<UserFollow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollow>()
                .HasOne(f => f.Followee)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
