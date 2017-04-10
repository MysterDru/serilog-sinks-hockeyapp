using System;
using System.Collections.Generic;
using System.IO;
using HockeyApp;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.HockeyApp
{
	public class HockeyAppSink : ILogEventSink
	{
		readonly ITextFormatter _textFormatter;

		public HockeyAppSink(ITextFormatter formatter)
		{
			_textFormatter = formatter;
		}

		public void Emit(LogEvent logEvent)
		{
			if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

			Dictionary<string, string> propertys = new Dictionary<string, string>();
			foreach (var property in logEvent.Properties)
			{
				propertys.Add(property.Key, property.Value.ToString());
			}
			propertys.Add(nameof(logEvent.Timestamp), logEvent.Timestamp.ToString());

			var renderSpace = new StringWriter();
			_textFormatter.Format(logEvent, renderSpace);

			MetricsManager.TrackEvent(renderSpace.ToString(), propertys, null);
		}
	}
}