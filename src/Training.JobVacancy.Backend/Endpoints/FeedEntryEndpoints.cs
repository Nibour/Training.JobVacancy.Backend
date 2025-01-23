using Adaptit.Training.JobVacancy.Backend.Dto;

using Microsoft.AspNetCore.Http.HttpResults;

namespace Adaptit.Training.JobVacancy.Backend.Endpoints;

public static class FeedEntryEndpoints
{
  public static IEndpointRouteBuilder MapFeedEntryEndpoints(this IEndpointRouteBuilder endpoints)
  {
    RouteGroupBuilder api = endpoints.MapGroup("/api/v2/feedentry")
      .WithOpenApi();

    _ = api.MapGet("/{entryId}",(string entryId) => GetEntryById(entryId));

    return endpoints;
  }


  private static async Task<Results<Ok<FeedEntry>,NotFound>> GetEntryById(string last)
  {
    return TypedResults.Ok(new FeedEntry("","","","","",null));
  }
}
