namespace Adaptit.Training.JobVacancy.Backend.Dto;

public record FeedEntryDto(
  string Uuid,
  string Status,
  string Title,
  string BusinessName,
  string Municipal,
  DateTimeOffset? SistEndret
);
