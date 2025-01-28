namespace Adaptit.Training.JobVacancy.Backend.Service.Background;

using Adaptit.Training.JobVacancy.Backend.Dto;
using Adaptit.Training.JobVacancy.PamStillingApi;

using Refit;

public class PamStillingApiCallBackgroundService(IPamStillingApi pamStillingApi, ILogger<PamStillingApiCallBackgroundService> logger) : BackgroundService
{
  private FeedDto? _firstFeed;
  private FeedDto? _lastFeed;
  /// <inheritdoc />
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      var response = await pamStillingApi.GetFeed();
      if (response is { IsSuccessStatusCode: true })
      {
        _firstFeed = response.Content;
      }
      else
      {
        HandleError(response.Error);
      }
      response = await pamStillingApi.GetFeed("last");
      if (response is { IsSuccessStatusCode: true})
      {
        _lastFeed = response.Content;
      }
      else
      {
        HandleError(response.Error);
      }
      await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
    }
  }

  private void HandleError(ApiException responseError) =>
    logger.LogError("Error occurred while fetching feed: {Message}", responseError.Message);

  public FeedDto? GetFeed(bool last) => last ? _lastFeed : _firstFeed;
}
