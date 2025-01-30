namespace Adaptit.Training.JobVacancy.Backend.Dto;

public class Feed
{
  public string Version { get; set; }
  public string Title { get; set; }
  public Uri HomePageUrl { get; set; }
  public string Description { get; set; }
  public Guid Id { get; set; }
  public Feed NextFeed { get; set; }
  public ICollection<FeedLine> Items { get; set; } = [];
}

