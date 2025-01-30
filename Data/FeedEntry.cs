namespace Adaptit.Training.JobVacancy.Backend.Dto;

public record FeedEntry(
  string Uuid,
  string Status,
  string Title,
  string BusinessName,
  string Municipal,
  DateTime? SistEndret
);
