using System.Collections.Specialized;

namespace BlazorApp1.Server;

public interface IMeasureNotifier {
	public event NotifyMeasuredEventHandler? Measured;
	public delegate void NotifyMeasuredEventHandler(MeasuredEventArgs e);
	public readonly static int Count = 16;
	public sealed class MeasuredEventArgs {
		public DateTime DateTime { get; set; }
		public double[] Values { get; set; } = new double[16];
	}
}
