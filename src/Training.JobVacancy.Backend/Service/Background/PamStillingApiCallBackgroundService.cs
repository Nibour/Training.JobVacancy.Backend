
using Adaptit.Training.JobVacancy.Backend.Dto;
using Adaptit.Training.JobVacancy.PamStillingApi;

namespace Adaptit.Training.JobVacancy.Backend.Service.Background;
public class PamStillingApiCallBackgroundService(IPamStillingApi pamStillingApi, ILogger<PamStillingApiCallBackgroundService> logger) : BackgroundService
{
  private FeedDto? _firstFeed;
  private FeedDto? _lastFeed;
  /// <inheritdoc />
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      await Task.WhenAll(GetFirstFeedAsync(), GetFirstFeedAsync("last"));
      await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
    }
  }

  private async Task GetFirstFeedAsync(string? last = null)
  {
    var response = await pamStillingApi.GetFeed(last);
    if (response is not { IsSuccessStatusCode: true })
    {
      logger.LogError(response.Error,"Error occurred while fetching feed: {Message}", response.Error.Message);
      return;
    }
    // todo: Implement DB logic
    if (last == "last")
    {
      _lastFeed = response.Content;
    }
    else
    {
      _firstFeed = response.Content;
    }
  }

    public FeedDto? GetFeed(bool last)
    {
        return last ? _lastFeed : _firstFeed;
    }
}
