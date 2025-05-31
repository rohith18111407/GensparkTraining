using Microsoft.EntityFrameworkCore;
using MoodJournal.Models;

namespace MoodJournal.Contexts
{
    public class MoodJournalContext : DbContext
    {
        public MoodJournalContext(DbContextOptions<MoodJournalContext> options) : base(options) {}

        public DbSet<MoodEntry> MoodEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoodEntry>(entity =>
            {
                entity.ToTable("mood_entries");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Mood).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Note).HasMaxLength(1000);
                entity.Property(e => e.Tip).HasMaxLength(1000);
            });
        }
    }
}