using BenchmarkDotNet.Attributes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BenchMark;

[MemoryDiagnoser(false)]
public class BenchMarkDemo
{
    private readonly MediatR.IMediator _mediatr;
    private readonly global::Mediator.IMediator _mediator;

    public BenchMarkDemo()
    {
        #region MediatR

        var mediatRServiceCollection = new ServiceCollection();
        mediatRServiceCollection.AddMediatR(configuration => configuration.AsTransient(), 
            typeof(MediatRExampleV1.Weather.WeatherForecast));
            
        var mediatRSerivceProvider = mediatRServiceCollection.BuildServiceProvider();
        _mediatr = mediatRSerivceProvider.GetRequiredService<IMediator>();

        #endregion

        #region Mediator

        var mediatorServiceCollection = new ServiceCollection();
        mediatorServiceCollection.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);
           
        var mediatorServiceProvider = mediatorServiceCollection.BuildServiceProvider();
        _mediator = mediatorServiceProvider.GetRequiredService<global::Mediator.IMediator>();

        #endregion
        
    }
    
    [Benchmark]
    public async Task<MediatRExampleV1.Weather.WeatherForecast[]> Weather_MediatR()
    {
        var request = MediatRExampleV1.Weather.WeatherForecastRequest.Instance;
        var result = await _mediatr.Send(request);
        return result.ToArray();
    }

    [Benchmark]
    public async Task<MediatRExampleV2.Weather.WeatherForecast[]> Weather_Mediator()
    {
        var request = MediatRExampleV2.Weather.WeatherForecastRequest.Instance;
        var result= await _mediator.Send(request);
        return result.ToArray();

    }
}