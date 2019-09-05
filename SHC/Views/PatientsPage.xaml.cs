using SHC.Models;
using SHC.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for PatientsPage.xaml
	/// </summary>
	public partial class PatientsPage : Page
	{
		PatientsPageViewModel ViewModel;

		public PatientsPage()
		{
			InitializeComponent();			
		}

		private void ButtonNewPatient_Click(object sender, RoutedEventArgs e)
		{
			App.MainFrame.Navigate(new PatientPage(null));
		}

		private void ListViewPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			App.MainFrame.Navigate(new PatientPage((Patient)ListViewPatients.SelectedItem));
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel = new PatientsPageViewModel();
			DataContext = ViewModel;
		}
	}
}
