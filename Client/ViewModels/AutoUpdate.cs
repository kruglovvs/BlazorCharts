using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BlazorApp1.Server;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Client.ViewModels;

public partial class ViewModel : ObservableObject {
	private List<ObservableCollection<TimeSpanPoint>> _observableCollections { get; set; } = new();
	public ObservableCollection<ISeries> SeriesCollections { get; set; } = new();

	[RelayCommand]
	public void AddItem(KeyValuePair<TimeSpan, double[]> item) {
		for (int i = 0; i < item.Value.Length; ++i) {
			if (i >= _observableCollections.Count) {
				_observableCollections.Add(new ObservableCollection<TimeSpanPoint>());
				SeriesCollections.Add(new LineSeries<TimeSpanPoint> {
					Values = _observableCollections[i],
					Fill = null,
					GeometrySize = 0,
					LineSmoothness = 0
				});
			}
			_observableCollections[i].Add(new(item.Key, item.Value[i]));
		}
	}
	[RelayCommand]
	public void DeleteItem() {
		for (int i = 0; i < _observableCollections.Count; ++i) {
			if (i >= _observableCollections.Count) {
				_observableCollections[i].RemoveAt(0);
			}
		}
	}

	public Axis[] XAxes { get; set; } =
	{
		new Axis
		{
			Labeler = value => value.AsTimeSpan().ToString(@"hh\:mm\:ss"),
			MinStep = TimeSpan.FromSeconds(10).TotalMinutes,
		}
	};

}



// use the line smoothness property to control the curve
// it goes from 0 to 1
// where 0 is a straight line and 1 the most curved

// when using a date time type, let the library know your unit 
//UnitWidth = TimeSpan.FromMilliseconds(1).Ticks, 

// if the difference between our points is in hours then we would:
// UnitWidth = TimeSpan.FromHours(1).Ticks,

// since all the months and years have a different number of days
// we can use the average, it would not cause any visible error in the user interface
// Months: TimeSpan.FromDays(30.4375).Ticks
// Years: TimeSpan.FromDays(365.25).Ticks

// The MinStep property forces the separator to be greater than 1 ms.
//MinStep = TimeSpan.FromMilliseconds(1).Ticks,