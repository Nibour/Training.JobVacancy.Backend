using Microsoft.EntityFrameworkCore;

namespace Adaptit.Data;

using Adaptit.Training.JobVacancy.Backend.Dto;

public class FeedDatabase(DbContextOptions<FeedDatabase> options) : DbContext(options)
{
  public DbSet<Feed> Feeds { get; set; }
  public DbSet<FeedEntry> FeedEntries { get; set; }
  public DbSet<FeedLine> FeedLines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     base.OnModelCreating(modelBuilder);
      _ = modelBuilder.Entity<FeedLine>()
          .HasOne(f => f._FeedEntry)
          .WithMany()
          .HasForeignKey("FeedEntryId");
    }
}


