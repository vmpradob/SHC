using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for DisabilitiesWindow.xaml
	/// </summary>
	public partial class DisabilitiesWindow : Window
	{
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<Disability> Disabilities { get; set; }

		public DisabilitiesWindow()
		{
			InitializeComponent();
			DataContext = this;
			Disabilities = new ObservableCollection<Disability>(App.DbContext.Disabilities);
			AreButtonsEnabled = true;
		}

		private void ButtonAddDisability_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxDisabilityName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}
			
			Disability disability = new Disability()
			{
				Name = name
			};

			App.DbContext.Disabilities.Add(disability);

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

			MessageBox.Show("Discapacidad agregada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxDisabilityName.Text = string.Empty;
			Disabilities.Add(disability);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteDisability_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			Disability disability = (Disability)button.DataContext;

			App.DbContext.Disabilities.Remove(disability);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Discapacidad eliminada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Disabilities.Remove(disability);
			AreButtonsEnabled = true;
		}
	}
}
