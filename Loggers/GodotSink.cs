using Serilog.Core;
using Serilog.Events;
using System;

namespace TechTutoria.GodotShared.Loggers;

/// <summary>
/// Sink for Serilog logging framework to support writing messages to Godot's IDE.
/// </summary>
/// <param name="formatProvider">Format provider.</param>
public class GodotSink(IFormatProvider formatProvider = null) : ILogEventSink
{
	private readonly IFormatProvider _formatProvider = formatProvider;

	/// <inheritdoc />
	public void Emit(LogEvent logEvent)
	{
		var message = logEvent.RenderMessage(_formatProvider);
		var formattedMessage = $"[{logEvent.Timestamp:yyyy-MM-dd HH:mm:ss}] [{logEvent.Level}] {message}";
		switch (logEvent.Level)
		{
			case LogEventLevel.Error:
			case LogEventLevel.Fatal:
				GD.PrintErr(formattedMessage);
				break;

			case LogEventLevel.Warning:
				GD.PushWarning(formattedMessage);
				break;

			case LogEventLevel.Information:
			case LogEventLevel.Debug:
			case LogEventLevel.Verbose:
				GD.Print(formattedMessage);
				break;
		}
	}
}
