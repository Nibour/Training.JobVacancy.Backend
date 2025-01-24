namespace Adaptit.Training.JobVacancy.Backend.Endpoints;

using Adaptit.Training.JobVacancy.Backend.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

public static class FeedEntryEndpoints
{
  public static IEndpointRouteBuilder MapFeedEntryEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var api = endpoints.MapGroup("/api/v2/feedentry")
      .WithOpenApi();

    _ = api.MapGet("/{entryId}",GetEntryById);

    return endpoints;
  }


  private static async Task<Results<Ok<FeedEntry>,NotFound>> GetEntryById(string entryId)
  {
    return TypedResults.Ok(new FeedEntry(entryId,"","","","",null));
  }
}
