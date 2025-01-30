namespace Adaptit.Training.JobVacancy.Backend.Dto;

public record FeedLineDto(
  Guid Id,
  Uri Url,
  string Title,
  string ContentText,
  DateTime? DateModified,
  FeedEntryDto? _FeedEntry
);
