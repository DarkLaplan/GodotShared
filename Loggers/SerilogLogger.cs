using Godot;
using Serilog;
using System.IO;

namespace TechTutoria.GodotShared.Loggers;

/// <summary>
/// Logger for logging messages through serilog framework.
/// </summary>
public partial class SerilogLogger : Node
{
	private const string OutputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] {Message}{NewLine}{Exception}";

	/// <inheritdoc />
	public override void _Ready()
	{
		var userSettingsGameDirectory = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Roaming", "Godot", "app_userdata", "Skokimons", "logs");
		Serilog.Log.Logger = new LoggerConfiguration()
			.WriteTo.File(userSettingsGameDirectory, outputTemplate: OutputTemplate, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10485760)
			.WriteTo.Sink(new GodotSink())
			.CreateLogger();
	}

	/// <inheritdoc />
	public override void _ExitTree()
	{
		Serilog.Log.CloseAndFlush();
	}

	/// <summary>
	/// Logs information.
	/// </summary>
	/// <param name="message">Message to log as information.</param>
	/// <param name="propertyValues">Optional values filled into message if contains template placeholders.</param>
	public static void Information(string message, params object[] propertyValues)
	{
		Serilog.Log.Information(message, propertyValues);
	}

	/// <summary>
	/// Logs Warning.
	/// </summary>
	/// <param name="message">Message to log as error.</param>
	/// <param name="propertyValues">Optional values filled into message if contains template placeholders.</param>
	public static void Warning(string message, params object[] propertyValues)
	{
		Serilog.Log.Warning(message, propertyValues);
	}

	/// <summary>
	/// Logs error.
	/// </summary>
	/// <param name="message">Message to log as error.</param>
	/// <param name="propertyValues">Optional values filled into message if contains template placeholders.</param>
	public static void Error(string message, params object[] propertyValues)
	{
		Serilog.Log.Error(message, propertyValues);
	}
}
