using Adaptit.Training.JobVacancy.Backend.Endpoints;
using Adaptit.Training.JobVacancy.Backend.Service.Background;
using Adaptit.Training.JobVacancy.PamStillingApi;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using Refit;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<OpenIdConnectOptions>();
builder.Services.AddTransient<OpenIdConnectOptions>();
builder.Services.AddScoped<OpenIdConnectOptions>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddHostedService<NavJobApiCallBackgroundService>(sp => sp.GetRequiredService<NavJobApiCallBackgroundService>());


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
app.MapFeedEndpoints(app.Services.GetRequiredService<NavJobApiCallBackgroundService>());
app.MapFeedEntryEndpoints();

app.Run();
