using Adaptit.Training.JobVacancy.Backend.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Adaptit.Training.JobVacancy.Backend.Endpoints;

public static class FeedEndpoints
{
  public static IEndpointRouteBuilder MapFeedEndpoints(this IEndpointRouteBuilder endpoints)
  {
    RouteGroupBuilder api = endpoints.MapGroup("/api/v2/feed")
        .WithOpenApi();

    _ = api.MapGet("", (HttpRequest request, string last) =>
    {
      string? ifNoneMatch = request.Headers["If-None-Match"].FirstOrDefault();
      string? ifModifiedSince = request.Headers["If-Modified-Since"].FirstOrDefault();

      return GetFirstOrLastPage(last, ifNoneMatch, ifModifiedSince);
    });

    _ = api.MapGet("/{feedPageId}", (HttpRequest request, string feedPageId) =>
    {
      string? ifNoneMatch = request.Headers["If-None-Match"].FirstOrDefault();
      string? ifModifiedSince = request.Headers["If-Modified-Since"].FirstOrDefault();

      return GetPageById(feedPageId, ifNoneMatch, ifModifiedSince);
    });

    return endpoints;
  }

  private static async Task<Results<Ok<Feed>, NotFound, StatusCodeHttpResult>> GetFirstOrLastPage(
      string last, string? ifNoneMatch, string? ifModifiedSince)
  {
    Feed feed = new(
        Version: "1.0",
        Title: "Example Feed",
        HomePageUrl: "https://example.com",
        FeedUrl: "https://example.com/feed",
        Description: "This is an example feed.",
        NextUrl: null,
        Id: "123",
        NextId: "456",
        Items: []
    );

    return TypedResults.Ok(feed);
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
