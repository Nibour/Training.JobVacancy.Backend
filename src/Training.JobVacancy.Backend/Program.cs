using Adaptit.Data;
using Adaptit.Training.JobVacancy.Backend.Endpoints;
using Adaptit.Training.JobVacancy.Backend.Options;
using Adaptit.Training.JobVacancy.Backend.Service.Background;
using Adaptit.Training.JobVacancy.PamStillingApi;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Refit;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<OpenIdConnectOptions>();
builder.Services.AddTransient<OpenIdConnectOptions>();
builder.Services.AddScoped<OpenIdConnectOptions>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddHostedService<PamStillingApiCallBackgroundService>();

builder.Services.AddDbContext<FeedDatabase>(options =>
{
    _ = options.UseNpgsql(builder.Configuration.GetConnectionString("FeedDatabase"),
      sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
      .EnableDetailedErrors()
      .EnableSensitiveDataLogging();
});

builder.Services.AddOptionsWithValidateOnStart<PamStillingOptions>()
  .BindConfiguration(PamStillingOptions.Section)
  .ValidateOnStart()
  .ValidateDataAnnotations();

builder.Services.AddRefitClient<IPamStillingApi>(provider =>
  {
    var options = provider.GetRequiredService<IOptions<PamStillingOptions>>();
    var settings = new RefitSettings() { AuthorizationHeaderValueGetter = (_,_) => Task.FromResult(options.Value.ApiKey), };
    return settings;
  })
  .ConfigureHttpClient(c =>
    c.BaseAddress = new Uri(builder.Configuration["PamStillingApi:BaseAddress"]!)
  );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapWeatherEndpoints();
app.MapFeedEndpoints();
app.MapFeedEntryEndpoints();

app.Run();
