namespace Adaptit.Training.JobVacancy.Backend.Dto;

public record FeedLineDto(
  string Id,
  string Url,
  string Title,
  string ContentText,
  DateTimeOffset? DateModified,
  FeedEntryDto? _FeedEntry
);
