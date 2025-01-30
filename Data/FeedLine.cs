namespace Adaptit.Training.JobVacancy.Backend.Dto;

public record FeedLine(
  string Id,
  string Url,
  string Title,
  string ContentText,
  DateTime? DateModified,
  Feed Feed,
  FeedEntry? _FeedEntry
);
