using System;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;

namespace Serilog
{
	/// <summary>
	/// Adds WriteTo.NSLog() to the logger configuration.
	/// </summary>
	public static class LoggerConfigurationHockeyAppExtensions
	{
		const string DefaultNSLogOutputTemplate = "[{Level}] {Message:l}{NewLine:l}{Exception:l}";

		/// <summary>
		/// Adds a sink that writes log events to HockeyApp events
		/// </summary>
		/// <param name="sinkConfiguration">The configuration being modified.</param>
		/// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
		/// <param name="outputTemplate">Template for the output events</param>
		/// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
		/// <exception cref="ArgumentNullException">A required parameter is null.</exception>
		public static LoggerConfiguration HockeyApp(this LoggerSinkConfiguration sinkConfiguration,
			LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
			string outputTemplate = DefaultNSLogOutputTemplate,
			IFormatProvider formatProvider = null)
		{

			if (sinkConfiguration == null)
				throw new ArgumentNullException("sinkConfiguration");

			if (outputTemplate == null)
				throw new ArgumentNullException("outputTemplate");

			var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
			return sinkConfiguration.Sink(new Sinks.HockeyApp.HockeyAppSink(formatter), restrictedToMinimumLevel);
		}
	}

}
