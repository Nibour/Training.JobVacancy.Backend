namespace Adaptit.Training.JobVacancy.Backend.Endpoints;
using Adaptit.Training.JobVacancy.Backend.Dto;
using Adaptit.Training.JobVacancy.Backend.Service.Background;
using Microsoft.AspNetCore.Http.HttpResults;

public static class FeedEndpoints
{

  public static void MapFeedEndpoints(this IEndpointRouteBuilder endpoints, NavJobApiCallBackgroundService navJobApiCallBackgroundService)
  {
    var api = endpoints.MapGroup("/api/v2/feed")
        .WithOpenApi();

    _ = api.MapGet("", (HttpRequest request, string last) =>
    {
      var ifNoneMatch = request.Headers.IfNoneMatch.FirstOrDefault();
      var ifModifiedSince = request.Headers.IfModifiedSince.FirstOrDefault();

      return GetFirstOrLastPage(last, ifNoneMatch, ifModifiedSince, navJobApiCallBackgroundService);
    });

    _ = api.MapGet("/{feedPageId}", (HttpRequest request, string feedPageId) =>
    {
      var ifNoneMatch = request.Headers.IfNoneMatch.FirstOrDefault();
      var ifModifiedSince = request.Headers.IfModifiedSince.FirstOrDefault();

      return GetPageById(feedPageId, ifNoneMatch, ifModifiedSince);
    });
  }

  private static async Task<Results<Ok<Feed>, NotFound, StatusCodeHttpResult>> GetFirstOrLastPage(
      string last, string? ifNoneMatch, string? ifModifiedSince, NavJobApiCallBackgroundService navJobApiCallBackgroundService)
  {
    var feed = last.Equals("last")
      ? navJobApiCallBackgroundService.GetFeed(true)
      : navJobApiCallBackgroundService.GetFeed(false);

    return feed != null
      ? TypedResults.Ok(feed)
      : TypedResults.NotFound();
  }

  private static async Task<Results<Ok<Feed>, NotFound, StatusCodeHttpResult>> GetPageById(
      string feedPageId, string? ifNoneMatch, string? ifModifiedSince)
  {

    Feed feed = new(
        Version: "1.0",
        Title: "Example Feed",
        HomePageUrl: "https://example.com",
        FeedUrl: "https://example.com/feed",
        Description: "This is an example feed.",
        NextUrl: null,
        Id: feedPageId,
        NextId: "456",
        Items: []
    );

    return TypedResults.Ok(feed);
  }
}
