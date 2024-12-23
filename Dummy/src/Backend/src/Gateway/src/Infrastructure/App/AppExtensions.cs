﻿namespace Makc2024.Dummy.Gateway.Infrastructure.App;

public static class AppExtensions
{
  public static IServiceCollection AddAppInfrastructureLayer(
    this IServiceCollection services,
    Microsoft.Extensions.Logging.ILogger logger,
    AppConfigOptions appConfigOptions,
    IConfiguration configuration,
    IConfigurationSection appConfigSection)
  {
    services.AddSerilog(config => config.ReadFrom.Configuration(configuration));

    services.Configure<AppConfigOptions>(appConfigSection);

    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

    services.AddScoped<IEventDispatcher, EventDispatcher>();

    services.AddScoped<AppSession>();

    services.AddTransient<IAppCommandService>(x =>
    {
      var appConfigOptions = x.GetRequiredService<IOptionsSnapshot<AppConfigOptions>>();

      return appConfigOptions.Value.Writer.Transport switch
      {
        AppTransport.Grpc => new AppGrpcCommandService(x.GetRequiredService<WriterAppGrpcClient>()),
        AppTransport.Http => new AppHttpCommandService(x.GetRequiredService<IHttpClientFactory>()),
        _ => throw new NotImplementedException()
      };
    });

    services.AddTransient<IDummyItemCommandService>(x =>
    {
      var appConfigOptions = x.GetRequiredService<IOptionsSnapshot<AppConfigOptions>>();

      return appConfigOptions.Value.Writer.Transport switch
      {
        AppTransport.Grpc => new DummyItemGrpcCommandService(
          x.GetRequiredService<WriterDummyItemGrpcClient>()),
        AppTransport.Http => new DummyItemHttpCommandService(
          x.GetRequiredService<IHttpClientFactory>()),
        _ => throw new NotImplementedException()
      };
    });

    services.AddTransient<IDummyItemQueryService>(x =>
    {
      var appConfigOptions = x.GetRequiredService<IOptionsSnapshot<AppConfigOptions>>();

      return appConfigOptions.Value.Writer.Transport switch
      {
        AppTransport.Grpc => new DummyItemGrpcQueryService(
          x.GetRequiredService<AppSession>(),
          x.GetRequiredService<WriterDummyItemGrpcClient>()),
        AppTransport.Http => new DummyItemHttpQueryService(
          x.GetRequiredService<AppSession>(),
          x.GetRequiredService<IHttpClientFactory>()),
        _ => throw new NotImplementedException()
      };
    });

    const string userAgent = nameof(Dummy);

    string writerRestApiAddress = appConfigOptions.Writer.RestApiAddress;
    
    Guard.Against.Empty(writerRestApiAddress, nameof(writerRestApiAddress));

    services.AddHttpClient(
      AppSettings.WriterDummyItemClientName,
      httpClient =>
      {
        httpClient.BaseAddress = new Uri(writerRestApiAddress);

        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
      })
      .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
      {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      });

    string writerGrpcApiAddress = appConfigOptions.Writer.GrpcApiAddress;

    services.AddGrpcClient<WriterAppGrpcClient>(
      AppSettings.WriterAppClientName,
      grpcOptions =>
      {
        grpcOptions.Address = new Uri(writerGrpcApiAddress);
      })
      .AddCallCredentials((context, metadata, serviceProvider) => Task.CompletedTask)
      .ConfigureChannel(grpcChannelOptions =>
      {
        grpcChannelOptions.UnsafeUseInsecureChannelCallCredentials = true;
      });

    services.AddGrpcClient<WriterDummyItemGrpcClient>(
      AppSettings.WriterDummyItemClientName,
      grpcOptions =>
      {
        grpcOptions.Address = new Uri(writerGrpcApiAddress);
      })
      .AddCallCredentials((context, metadata, serviceProvider) => Task.CompletedTask)
      .ConfigureChannel(grpcChannelOptions =>
      {
        grpcChannelOptions.UnsafeUseInsecureChannelCallCredentials = true;
      });

    logger.LogInformation("Infrastructure layer added");

    return services;
  }

  public static AppConfigOptions CreateAppConfigOptions(this IConfigurationSection appConfigSection)
  {
    var result = new AppConfigOptions();

    appConfigSection.Bind(result);

    return result;
  }
}
