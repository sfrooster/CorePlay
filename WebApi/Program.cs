using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(new HuggingService());
var app = builder.Build();



app.MapGet("/", () => "Hello World!");




async ValueTask<object?> Timestamp1(EndpointFilterInvocationContext ctx, EndpointFilterDelegate next)
{
  var result = await next(ctx);
  if (result is Ok<Hugged> { Value : { } } hugged)
    hugged.Value.Timestamp = DateTime.UtcNow;
  return result;
}

async ValueTask<object?> Timestamp2(EndpointFilterInvocationContext ctx, EndpointFilterDelegate next)
{
  var result = await next(ctx);
  if (result is Ok<Hugged> { Value : { } } hugged)
    hugged.Value.BestTeam = "Rams";
  return result;
}

app.MapPost("/hugs", (Hug hug, HuggingService hugger) =>
    Results.Ok(hugger?.Hug(hug))
  )
  .AddEndpointFilter(Timestamp1)
  .AddEndpointFilter(Timestamp2);


app.Run();
