using LiveCharts;
using LiveCharts.Wpf;
using SHC.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace SHC.Views.Statistics
{
	/// <summary>
	/// Interaction logic for CommunitiesPerSpecialty.xaml
	/// </summary>
	public partial class CommunitiesPerSpecialty : Page, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Specialty> Specialties { get; set; }
		public SeriesCollection SeriesCollection { get; set; }
		public string[] Labels { get; set; }
		public LiveCharts.Wpf.Separator Separator { get; set; }
		public Func<double, string> Formatter { get; set; }

		public CommunitiesPerSpecialty()
		{
			InitializeComponent();
			DataContext = this;

			Specialties = new ObservableCollection<Specialty>(App.DbContext.Specialties);

			Separator = new LiveCharts.Wpf.Separator()
			{
				Step = 1
			};

			Formatter = value => value.ToString("N");
		}

		private void UpdateGraph()
		{
			var diagnostic = ComboBoxSpecialties.Text;
			var diagnostics = App.DbContext.Appointments.Where(x => x.Doctor.Specialty.Name == diagnostic);

			var result = diagnostics.GroupBy(x => x.Patient.Address.Community.Name)
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

		private void ButtonSearch_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			UpdateGraph();
		}
	}
}
