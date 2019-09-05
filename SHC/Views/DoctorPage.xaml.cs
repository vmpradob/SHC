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
	/// Interaction logic for NewDoctorPage.xaml
	/// </summary>
	public partial class DoctorPage : Page
	{
		DoctorPageViewModel ViewModel;

		public DoctorPage(Doctor doctor)
		{
			InitializeComponent();
			ViewModel = new DoctorPageViewModel(doctor);
			DataContext = ViewModel;
		}

		private void ButtonSaveOrEdit_Click(object sender, RoutedEventArgs e)
		{
			switch (ViewModel.SaveOrEditDoctor())
			{
				case -2:
					MessageBox.Show("Éste médico ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case -1:
					MessageBox.Show("Debe ingresar todos los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case 1:
					MessageBox.Show("Médico creado/editado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
					break;
			}
		}

		private void ButtonNewSpecialty_Click(object sender, RoutedEventArgs e)
		{
			SpecialtiesWindow window = new SpecialtiesWindow();
			window.ShowDialog();
			ViewModel.UpdateSpecialties();
		}

		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
	}
}
