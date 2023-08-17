using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Messaging;
using static BlazorApp1.Server.IMeasureNotifier;

namespace BlazorApp1.Server;

public partial class MeasureNotifier : IMeasureNotifier {

	public event NotifyMeasuredEventHandler? Measured;
	public MeasureNotifier() {
		_timer.Interval = 1000;
		_timer.Elapsed += OnTimedEvent;
		_timer.AutoReset = true;
		_timer.Enabled = true;
	}
	private static readonly System.Timers.Timer _timer = new();
	private static double[] Measure() {
		Console.WriteLine("Measured");
		double[] values = new double[Count];
		for (int i = 0; i < Count; ++i) {
			values[i] = i + new Random().NextDouble();
		}
		return values;
	}
	private void OnTimedEvent(object? source, System.Timers.ElapsedEventArgs e) {
		double[] values = Measure();
		Measured?.Invoke(new MeasuredEventArgs() { DateTime = DateTime.Now, Values = values });
	}
}