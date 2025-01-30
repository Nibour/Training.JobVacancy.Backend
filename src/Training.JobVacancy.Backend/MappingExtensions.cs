namespace Adaptit.Training.JobVacancy.Backend;

using Adaptit.Training.JobVacancy.Backend.Dto;

public static class MappingExtensions
{
  public static Feed FromDto(this FeedDto source)
  {
    var feed = new Feed()
    {
      Id = source.Id, Title = source.Title, Description = source.Description
    };
    feed.Items = source.Items.Select(x => x.FromDto(feed)).ToList();
    return feed;
  }

  public static FeedLine FromDto(this FeedLineDto source, Feed feed)
  {

  }

}
