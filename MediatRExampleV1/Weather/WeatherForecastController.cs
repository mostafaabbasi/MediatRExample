using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatRExampleV1.Weather;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var result = await _mediator.Send(WeatherForecastRequest.Instance);

        return result;
    }
}

