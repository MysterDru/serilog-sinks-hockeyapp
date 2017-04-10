using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            if (MetricsManager.Disabled)
            {
                Debug.WriteLine($"ERROR: {nameof(HockeyAppSink)}: HockeyApp MetricsManager is disabled. Cannot write event to hockeyapp. Aborting.");
                return;
            }

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