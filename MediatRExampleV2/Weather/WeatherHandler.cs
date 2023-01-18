using Mediator;

namespace MediatRExampleV2.Weather;

public class WeatherHandler : IRequestHandler<WeatherForecastRequest, IEnumerable<WeatherForecast>>
{
    private static readonly string[] Summaries  =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public ValueTask<IEnumerable<WeatherForecast>> Handle(WeatherForecastRequest request, CancellationToken cancellationToken)
    {
        var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });

        return ValueTask.FromResult(result);
    }
}