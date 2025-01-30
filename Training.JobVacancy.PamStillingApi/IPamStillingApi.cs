
using Adaptit.Training.JobVacancy.Backend.Dto;

using Refit;

namespace Adaptit.Training.JobVacancy.PamStillingApi;

[Headers("Authorization: Bearer")]
public interface IPamStillingApi
{
  [Get("/api/v1/feed")]
  Task<ApiResponse<FeedDto>> GetFeed(string? last = null);

  [Get("/api/v1/{feedPageId}")]
  Task<ApiResponse<FeedDto>> GetFeedById(string feedPageId);
}
