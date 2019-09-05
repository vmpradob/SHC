using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace SHC.Views.Statistics
{
	/// <summary>
	/// Interaction logic for CommunitiesPerDiagnostic.xaml
	/// </summary>
	public partial class CommunitiesPerDiagnostic : Page, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public SeriesCollection SeriesCollection { get; set; }
		public string[] Labels { get; set; }
		public LiveCharts.Wpf.Separator Separator { get; set; }
		public Func<double, string> Formatter { get; set; }

		public CommunitiesPerDiagnostic()
		{
			InitializeComponent();
			DataContext = this;

			Separator = new LiveCharts.Wpf.Separator()
			{
				Step = 1
			};

			Formatter = value => value.ToString("N");
		}

		private void UpdateGraph()
		{
			var diagnostic = TextBoxDiagnostic.Text;
			var diagnostics = App.DbContext.Appointments.Where(x => x.Diagnostic.Contains(diagnostic));

			var result = diagnostics.GroupBy(x => x.Patient.Address.Community.Name)
				.Select(group => new {
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

		private void ButtonSearch_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			UpdateGraph();
		}
	}
}
