# Serilog.Sinks.HockeyApp 
A [Serilog](https://serilog.net) sink that writes events to [Hockey App](https://www.hockeyapp.net).

![vsts build](https://drewfrisk.visualstudio.com/_apis/public/build/definitions/fbc17324-598d-441f-8704-fb5d7b66a452/7/badge)
[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/serilog-sinks-hockeyapp/)

### Getting started

Configure in a shared location (either portable class library or shared library) with:

```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.HockeyApp(Serilog.Events.LogEventLevel.Information)
    .CreateLogger();
```

Within your portable class libary or shared code of your application log your event:

```csharp
Log.Information("This {EventName} will be written to HockeyApp", "Awesome Event");
```

Because the memory buffer may contain events that have not yet been written to the target sink, it is important to call `Log.CloseAndFlush()` or `Logger.Dispose()` when the application/activity exits.


### Additional Info

Due to restrictions in HockeyApp any properties contained in your log message will not be visible in the HockeyApp dashboard. If you [configure a bridge](https://support.hockeyapp.net/discussions/problems/65785-custom-events-properties) between HockeyApp and Application Insights your event properties will be visible.

See [here](https://support.hockeyapp.net/discussions/problems/65785-custom-events-properties), and [here](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-hockeyapp-bridge-app) for additional details on this.

For additional info on HockeyApp events, read the [HockeyApp docs](https://support.hockeyapp.net/kb/general-account-management-2/getting-started-with-custom-events-public-preview).

### About this sink

This sink is maintained by [Drew Frisk](http://drewfrisk.com/).
