namespace Adaptit.Training.JobVacancy.Backend.Endpoints;
using Adaptit.Training.JobVacancy.Backend.Dto;
using Adaptit.Training.JobVacancy.Backend.Service.Background;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public static class FeedEndpoints
{

  public static void MapFeedEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var api = endpoints.MapGroup("/api/v2/feed")
        .WithOpenApi();

    api.MapGet("", GetFirstOrLastPage);
    api.MapGet("/{feedPageId}", GetPageById);
  }

  private static async Task<Results<Ok<FeedDto>, NotFound, StatusCodeHttpResult>> GetFirstOrLastPage(
      string last,[FromHeader(Name = "if-None-Match")] string? ifNoneMatch, [FromHeader(Name = "if-Modified-Since")] string? ifModifiedSince, [FromServices] PamStillingApiCallBackgroundService pamStillingApiCallBackgroundService)
  {
    var feed = last.Equals("last")
      ? pamStillingApiCallBackgroundService.GetFeed(true)
      : pamStillingApiCallBackgroundService.GetFeed(false);

    return feed != null
      ? TypedResults.Ok(feed)
      : TypedResults.NotFound();
  }

  private static async Task<Results<Ok<FeedDto>, NotFound, StatusCodeHttpResult>> GetPageById(
      string feedPageId, string? ifNoneMatch, string? ifModifiedSince)
  {

   FeedDto feed = new(
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
