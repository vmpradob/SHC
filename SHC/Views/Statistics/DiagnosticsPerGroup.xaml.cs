using LiveCharts;
using LiveCharts.Wpf;
using SHC.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Statistics
{
	/// <summary>
	/// Interaction logic for DiagnosticsPerGroup.xaml
	/// </summary>
	public partial class DiagnosticsPerGroup : Page, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public SeriesCollection SeriesCollection { get; set; }
		public string[] Labels { get; set; }
		public LiveCharts.Wpf.Separator Separator { get; set; }
		public Func<double, string> Formatter { get; set; }

		public DiagnosticsPerGroup()
		{
			InitializeComponent();
			DataContext = this;

			var groups = new string[20];
			for (int i = 0; i < 20; i++)
			{
				if (i < 19)
				{
					groups[i] = string.Format("De {0} a {1} años", i * 5, i * 5 + 4);
				}
				else
				{
					groups[i] = string.Format("De {0} o más", i * 5);
				}
			}
			ComboBoxGroups.ItemsSource = groups;

			ComboBoxGenders.ItemsSource = new string[] { "Hombres", "Mujeres", "Ambos" };

			Separator = new LiveCharts.Wpf.Separator()
			{
				Step = 1
			};

			Formatter = value => value.ToString("N");
		}

		private void UpdateChart()
		{
			var selectedGroup = ComboBoxGroups.SelectedIndex + 1;
			var beginYear = DateTime.Now.Year - selectedGroup * 5 + 1;
			var endYear = DateTime.Now.Year - selectedGroup * 5 + 5;

			var diagnostics = App.DbContext.Appointments.Where(x => x.Patient.BirthDate.Year >= beginYear);

			if (selectedGroup < 20)
			{
				diagnostics = diagnostics.Where(x => x.Patient.BirthDate.Year <= endYear);
			}			

			if (ComboBoxGenders.SelectedIndex < 2)
			{
				if (ComboBoxGenders.SelectedIndex == 0)
				{
					diagnostics = diagnostics.Where(x => x.Patient.Gender == Gender.Male);
				}
				else
				{
					diagnostics = diagnostics.Where(x => x.Patient.Gender == Gender.Female);						
				}

				var result = diagnostics.GroupBy(x => x.Diagnostic)
				.Select(group => new
				{
					Name = group.Key,
					Count = group.Count()
				})
				.OrderByDescending(x => x.Count)
				.Take(10);

				int i = 0;
				Labels = new string[10];
				var values = new int[10];

				foreach (var line in result)
				{
					if (i == 10) { break; }
					Labels[i] = line.Name;
					values[i] = line.Count;
					i++;
				}

				SeriesCollection = new SeriesCollection
				{
					new ColumnSeries
					{
						Title = "",
						Values = new ChartValues<int> (values)
					}
				};
			}
			else
			{
				var result = diagnostics.GroupBy(x => x.Diagnostic)
				.Select(group => new
				{
					Name = group.Key,
					Count = group.Count()
				})
				.OrderByDescending(x => x.Count)
				.Take(10);

				int i = 0;
				Labels = new string[10];
				var maleValues = new int[10];
				var femaleValues = new int[10];

				foreach (var line in result)
				{
					if (i == 10) { break; }
					Labels[i] = line.Name;
					maleValues[i] = diagnostics.Where(x => x.Diagnostic == line.Name && x.Patient.Gender == Gender.Male).Count();
					femaleValues[i] = line.Count - maleValues[i];
					i++;
				}

				SeriesCollection = new SeriesCollection
				{
					new ColumnSeries
					{
						Title = "Hombres",
						Values = new ChartValues<int> (maleValues)
					},
					new ColumnSeries
					{
						Title = "Mujeres",
						Values = new ChartValues<int> (femaleValues)
					}
				};
			}
		}

		private void ButtonSearch_Click(object sender, RoutedEventArgs e)
		{
			UpdateChart();
		}
	}
}
