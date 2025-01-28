using Adaptit.Data;
using Adaptit.Training.JobVacancy.Backend.Endpoints;
using Adaptit.Training.JobVacancy.Backend.Service.Background;
using Adaptit.Training.JobVacancy.PamStillingApi;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;

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
  options.UseNpgsql(builder.Configuration.GetConnectionString("FeedDatabase"),
    sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging();
});

builder.Services.AddRefitClient<IPamStillingApi>()
  .ConfigureHttpClient(c =>
    c.BaseAddress = new Uri(builder.Configuration["PamStillingApi:BaseAddress"]!)
  );
//Na rwthsw ti den htan safe me to na perna ta data me to configuation

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
