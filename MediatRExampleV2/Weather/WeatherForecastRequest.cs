using Mediator;

namespace MediatRExampleV2.Weather;

public record WeatherForecastRequest : IRequest<IEnumerable<WeatherForecast>>
{
    public static readonly WeatherForecastRequest Instance = new ();
    
    private WeatherForecastRequest(){}
}