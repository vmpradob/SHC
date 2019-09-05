using SHC.Models;
using SHC.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for DoctorsPage.xaml
	/// </summary>
	public partial class DoctorsPage : Page
	{
		DoctorsPageViewModel ViewModel;

		public DoctorsPage()
		{
			InitializeComponent();
		}

		private void ButtonNewDoctor_Click(object sender, RoutedEventArgs e)
		{
			App.MainFrame.Navigate(new DoctorPage(null));
		}

		private void ListViewDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			App.MainFrame.Navigate(new DoctorPage((Doctor)ListViewDoctors.SelectedItem));
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel = new DoctorsPageViewModel();
			DataContext = ViewModel;
		}
	}
}
