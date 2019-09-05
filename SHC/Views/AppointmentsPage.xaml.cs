using SHC.Models;
using SHC.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for AppointmentsPage.xaml
	/// </summary>
	public partial class AppointmentsPage : Page
	{
		AppointmentsPageViewModel ViewModel;

		public AppointmentsPage()
		{
			InitializeComponent();			
		}

		private void ButtonNewAppointment_Click(object sender, RoutedEventArgs e)
		{
			App.MainFrame.Navigate(new AppointmentPage(null));
		}

		private void ListViewAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			App.MainFrame.Navigate(new AppointmentPage((Appointment)ListViewAppointments.SelectedItem));
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{ 
			ViewModel = new AppointmentsPageViewModel();
			DataContext = ViewModel;
		}
	}
}
