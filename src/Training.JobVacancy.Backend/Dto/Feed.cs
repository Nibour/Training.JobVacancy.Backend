namespace Adaptit.Training.JobVacancy.Backend.Dto;

public record Feed(
  string Version,
  string Title,
  string HomePageUrl,
  string FeedUrl,
  string Description,
  string? NextUrl,
  string Id,
  string NextId,
  List<FeedLine>? Items
);
