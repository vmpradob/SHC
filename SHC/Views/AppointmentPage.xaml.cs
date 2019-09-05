using SHC.Models;
using SHC.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for NewAppointmentPage.xaml
	/// </summary>
	public partial class AppointmentPage : Page
	{
		AppointmentPageViewModel ViewModel;

		public AppointmentPage(Appointment appointment)
		{
			InitializeComponent();
			ViewModel = new AppointmentPageViewModel(appointment);
			DataContext = ViewModel;
		}

		private void ComboBoxDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ViewModel.SelectDoctor();
		}

		private void ButtonSearchPatient_Click(object sender, RoutedEventArgs e)
		{
			switch (ViewModel.SearchPatient(TextBoxPatiendIdCard.Text, CheckBoxRepresentant.IsChecked))
			{				
				case -1:
					MessageBox.Show("No se encontró dicho paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
			}
		}

		private void ButtonSaveAppointment_Click(object sender, RoutedEventArgs e)
		{
			switch (ViewModel.SaveAppointment(TextBoxReasonForConsultation.Text, TextBoxDiagnostic.Text, TextBoxTreatment.Text, TextBoxObservations.Text))
			{
				case -1:
					MessageBox.Show("Debe ingresar todos los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case 0:
					MessageBox.Show("Cita registrada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
					App.MainFrame.GoBack();
					break;
			}
		}

		private void ButtonAddTest_Click(object sender, RoutedEventArgs e)
		{
			switch (ViewModel.AddTest(TextBoxTestName.Text, TextBoxTestResult.Text, TextBoxTestObservation.Text))
			{
				case -1:
					MessageBox.Show("Debe ingresar todos los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case 0:
					TextBoxTestName.Text = string.Empty;
					TextBoxTestObservation.Text = string.Empty;
					TextBoxTestResult.Text = string.Empty;
					break;
			}			
		}

		private void ButtonDeleteTest_Click(object sender, RoutedEventArgs e)
		{
			Button button = ((Button)sender);
			ViewModel.DeleteTest((Test)button.DataContext);
		}
	}
}
