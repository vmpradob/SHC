using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for EducationLevelsWindow.xaml
	/// </summary>
	public partial class EducationLevelsWindow : Window
	{
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<EducationLevel> EducationLevels { get; set; }

		public EducationLevelsWindow()
		{
			InitializeComponent();
			DataContext = this;
			EducationLevels = new ObservableCollection<EducationLevel>(App.DbContext.EducationLevels);
			AreButtonsEnabled = true;
		}

		private void ButtonAddEducationLevel_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxEducationLevelName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			EducationLevel educationLevel = new EducationLevel()
			{
				Name = name
			};

			App.DbContext.EducationLevels.Add(educationLevel);

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

			MessageBox.Show("Escolaridad agregada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxEducationLevelName.Text = string.Empty;
			EducationLevels.Add(educationLevel);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteEducationLevel_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			EducationLevel educationLevel = (EducationLevel)button.DataContext;

			App.DbContext.EducationLevels.Remove(educationLevel);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Escolaridad eliminada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			EducationLevels.Remove(educationLevel);
			AreButtonsEnabled = true;
		}
	}
}
