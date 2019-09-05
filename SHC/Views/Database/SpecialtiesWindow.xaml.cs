using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for SpecialtiesWindow.xaml
	/// </summary>
	public partial class SpecialtiesWindow : Window
	{
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<Specialty> Specialties { get; set; }

		public SpecialtiesWindow()
		{
			InitializeComponent();
			DataContext = this;
			Specialties = new ObservableCollection<Specialty>(App.DbContext.Specialties);
			AreButtonsEnabled = true;
		}

		private void ButtonAddSpecialty_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxSpecialtyName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Specialty specialty = new Specialty()
			{
				Name = name
			};

			App.DbContext.Specialties.Add(specialty);

			try
			{
				App.DbContext.SaveChanges();
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			MessageBox.Show("Especialidad agregada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxSpecialtyName.Text = string.Empty;
			Specialties.Add(specialty);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteSpecialty_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			Specialty specialty = (Specialty)button.DataContext;

			App.DbContext.Specialties.Remove(specialty);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Especialidad eliminada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Specialties.Remove(specialty);
			AreButtonsEnabled = true;
		}
	}
}
