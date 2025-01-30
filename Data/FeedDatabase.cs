using Microsoft.EntityFrameworkCore;

using Adaptit.Training.JobVacancy.Backend.Dto;

namespace Adaptit.Data;
public class FeedDatabase(DbContextOptions<FeedDatabase> options) : DbContext(options)
{
  public DbSet<Feed> Feeds { get; set; }
  public DbSet<FeedEntry> FeedEntries { get; set; }
  public DbSet<FeedLine> FeedLines { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     _ = modelBuilder.Entity<Feed>()
    //       .HasMany(f => f.Items)
    //       .WithOne()
    //       .HasForeignKey("FeedId");
    //
    //     _ = modelBuilder.Entity<FeedLine>()
    //       .HasOne(f => f._FeedEntry)
    //       .WithMany()
    //       .HasForeignKey("FeedEntryId");
    // }
}


