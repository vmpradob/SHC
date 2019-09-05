using LiveCharts;
using LiveCharts.Wpf;
using SHC.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Statistics
{
	/// <summary>
	/// Interaction logic for DiagnosticsPerRegion.xaml
	/// </summary>
	public partial class DiagnosticsPerRegion : Page, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Parish> Parishes { get; set; }
		public ObservableCollection<Community> Communities { get; set; }
		public ObservableCollection<Sector> Sectors { get; set; }
		public ObservableCollection<Street> Streets { get; set; }
		public SeriesCollection SeriesCollection { get; set; }
		public string[] Labels { get; set; }
		public LiveCharts.Wpf.Separator Separator { get; set; }
		public Func<double, string> Formatter { get; set; }

		public DiagnosticsPerRegion()
		{
			InitializeComponent();
			DataContext = this;

			Parishes = new ObservableCollection<Parish>(App.DbContext.Parishes);
			ComboBoxParish.SelectedIndex = 0;

			Separator = new LiveCharts.Wpf.Separator()
			{
				Step = 1
			};

			Formatter = value => value.ToString("N");
		}

		private void UpdateGraph()
		{
			var parish = (Parish)ComboBoxParish.SelectedItem;
			var diagnostics = App.DbContext.Appointments.Where(x => x.Patient.Address.Parish.Id == parish.Id);

			if (ComboBoxCommunity.SelectedItem != null)
			{
				var community = (Community)ComboBoxCommunity.SelectedItem;

				if (ComboBoxSector.SelectedItem != null)
				{
					var sector = (Sector)ComboBoxSector.SelectedItem;

					if (ComboBoxStreet.SelectedItem != null)
					{
						var street = (Street)ComboBoxStreet.SelectedItem;
						diagnostics = diagnostics.Where(x => x.Patient.Address.Street.Id == street.Id);
					}
					else
					{
						diagnostics = diagnostics.Where(x => x.Patient.Address.Sector.Id == sector.Id);
					}
				}
				else
				{
					diagnostics = diagnostics.Where(x => x.Patient.Address.Community.Id == community.Id);
				}
			}

			var result = diagnostics.GroupBy(x => x.Diagnostic)
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

		private void UpdateCommunities()
		{
			var parish = (Parish)ComboBoxParish.SelectedItem;

			if (parish != null)
			{
				Communities = new ObservableCollection<Community>(App.DbContext.Communities.Where(x => x.Parish.Id == parish.Id));
			}
			else
			{
				Communities = null;
			}
			UpdateSectors();
		}

		private void UpdateSectors()
		{
			var community = (Community)ComboBoxCommunity.SelectedItem;

			if (community != null)
			{
				Sectors = new ObservableCollection<Sector>(App.DbContext.Sectors.Where(x => x.Community.Id == community.Id));
			}
			else
			{
				Sectors = null;
			}
			UpdateStreets();
		}

		private void UpdateStreets()
		{
			var sector = (Sector)ComboBoxSector.SelectedItem;

			if (sector != null)
			{
				Streets = new ObservableCollection<Street>(App.DbContext.Streets.Where(x => x.Sector.Id == sector.Id));
			}
			else
			{
				Streets = null;
			}
		}

		private void ComboBoxParish_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateGraph();
			ComboBoxCommunity.SelectedItem = null;
			UpdateCommunities();
		}
		
		private void ComboBoxCommunity_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateGraph();
			ComboBoxSector.SelectedItem = null;
			UpdateSectors();
		}

		private void ButtonRemoveCommunity_Click(object sender, RoutedEventArgs e)
		{
			ComboBoxCommunity.SelectedItem = null;
			UpdateGraph();
		}

		private void ComboBoxSector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateGraph();
			ComboBoxStreet.SelectedItem = null;
			UpdateStreets();
		}

		private void ButtonRemoveSector_Click(object sender, RoutedEventArgs e)
		{
			ComboBoxSector.SelectedItem = null;
			UpdateGraph();
		}

		private void ComboBoxStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateGraph();
		}

		private void ButtonRemoveStreet_Click(object sender, RoutedEventArgs e)
		{
			ComboBoxStreet.SelectedItem = null;
			UpdateGraph();
		}
	}
}
