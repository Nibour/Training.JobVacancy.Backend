namespace Adaptit.Training.JobVacancy.Backend.Service.Background;

using Adaptit.Training.JobVacancy.Backend.Dto;
using Adaptit.Training.JobVacancy.PamStillingApi;

public class NavJobApiCallBackgroundService(IPamStillingApi pamStillingApi) : BackgroundService
{
  private Feed? _firstFeed;
  private Feed? _lastFeed;
  /// <inheritdoc />
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      var response = await pamStillingApi.GetFeed();
      if (response is { IsSuccessStatusCode: true })
      {
        _firstFeed = response.Content; // Extract content and convert to List<Feed>
      }
      else
      {
        HandleError();
      }
      response = await pamStillingApi.GetFeed("last");
      if (response is { IsSuccessStatusCode: true})
      {
        _lastFeed = response.Content; // Extract content and convert to List<Feed>
      }
      else
      {
        HandleError();
      }
      await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
    }
  }

  private static void HandleError() =>
    // TODO: ADD LOG OF FAILURE
    throw new NotImplementedException();

  public Feed? GetFeed(bool last) => last ? _lastFeed : _firstFeed;
}
