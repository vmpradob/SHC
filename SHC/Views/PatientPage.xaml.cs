using SHC.Models;
using SHC.ViewModels;
using SHC.Views.Database;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for NewPatientPage.xaml
	/// </summary>
	public partial class PatientPage : Page
	{
		PatientPageViewModel ViewModel;

		public PatientPage(Patient patient)
		{
			InitializeComponent();
			ViewModel = new PatientPageViewModel(patient);
			DataContext = ViewModel;
		}

		private void ButtonSaveOrEdit_Click(object sender, RoutedEventArgs e)
		{
			switch (ViewModel.SaveOrEditPatient())
			{
				case -2:
					MessageBox.Show("Éste paciente ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case -1:
					MessageBox.Show("Debe ingresar todos los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case 1:
					MessageBox.Show("Paciente creado/editado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
					break;
			}
		}

		private void ComboBoxParish_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ViewModel.UpdateCommunities();
			ComboBoxCommunity.SelectedItem = null;
		}

		private void ButtonNewParish_Click(object sender, RoutedEventArgs e)
		{
			ParishesWindow window = new ParishesWindow();
			window.ShowDialog();
			ViewModel.UpdateParishes();
		}

		private void ComboBoxCommunity_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ViewModel.UpdateSectors();
			ComboBoxSector.SelectedItem = null;
		}

		private void ButtonNewCommunity_Click(object sender, RoutedEventArgs e)
		{
			CommunitiesWindow window = new CommunitiesWindow();
			window.ShowDialog();
			ViewModel.UpdateCommunities();
		}

		private void ComboBoxSector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ViewModel.UpdateStreets();
			ComboBoxStreet.SelectedItem = null;
		}

		private void ButtonNewSector_Click(object sender, RoutedEventArgs e)
		{
			SectorsWindow window = new SectorsWindow();
			window.ShowDialog();
			ViewModel.UpdateSectors();
		}

		private void ButtonNewStreet_Click(object sender, RoutedEventArgs e)
		{
			StreetsWindow window = new StreetsWindow();
			window.ShowDialog();
			ViewModel.UpdateStreets();
		}

		private void ButtonNewEducationLevel_Click(object sender, RoutedEventArgs e)
		{
			EducationLevelsWindow window = new EducationLevelsWindow();
			window.ShowDialog();
			ViewModel.UpdateEducationsLevels();
		}

		private void ButtonNewDisability_Click(object sender, RoutedEventArgs e)
		{
			DisabilitiesWindow window = new DisabilitiesWindow();
			window.ShowDialog();
			ViewModel.UpdateDisabilities();
		}

		private void ButtonAddDisability_Click(object sender, RoutedEventArgs e)
		{
			if (ComboBoxDisabilities.SelectedItem != null)
			{
				ViewModel.AddDisability((Disability)ComboBoxDisabilities.SelectedItem);
				ComboBoxDisabilities.SelectedItem = null;
			}
		}

		private void ButtonDeleteDisability_Click(object sender, RoutedEventArgs e)
		{
			Button button = ((Button)sender);
			ViewModel.DeleteDisability((Disability)button.DataContext);
		}

		private void ButtonNewAntecedent_Click(object sender, RoutedEventArgs e)
		{
			AntecedentsWindow window = new AntecedentsWindow();
			window.ShowDialog();
			ViewModel.UpdateAntecedents();
		}

		private void ButtonAddAntecedent_Click(object sender, RoutedEventArgs e)
		{
			if (ComboBoxAntecedents.SelectedItem != null)
			{
				ViewModel.AddAntecedent((Antecedent)ComboBoxAntecedents.SelectedItem);
				ComboBoxAntecedents.SelectedItem = null;
			}
		}

		private void ButtonDeleteAntecedent_Click(object sender, RoutedEventArgs e)
		{
			Button button = ((Button)sender);
			ViewModel.DeleteAntecedent((Antecedent)button.DataContext);
		}

		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
	}
}
