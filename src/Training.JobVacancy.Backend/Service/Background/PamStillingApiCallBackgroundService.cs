namespace Adaptit.Training.JobVacancy.Backend.Service.Background;

using Adaptit.Training.JobVacancy.Backend.Dto;
using Adaptit.Training.JobVacancy.PamStillingApi;

using Refit;

public class PamStillingApiCallBackgroundService(IPamStillingApi pamStillingApi) : BackgroundService
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

  private static void HandleError(ApiException responseError) =>
    // TODO: ADD LOG OF FAILURE, A BACKGROUND SERVICE SHOULD NEVER THROW EXCEPTION FOR IT MUST CONTINUE TO RUN
    throw new NotImplementedException();

  public Feed? GetFeed(bool last) => last ? _lastFeed : _firstFeed;
}
