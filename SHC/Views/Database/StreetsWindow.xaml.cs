using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for StreetsWindow.xaml
	/// </summary>
	public partial class StreetsWindow : Window
	{
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<Sector> Sectors { get; set; }
		public ObservableCollection<Street> Streets { get; set; }

		public StreetsWindow()
		{
			InitializeComponent();
			DataContext = this;
			Sectors = new ObservableCollection<Sector>(App.DbContext.Sectors);
			Streets = new ObservableCollection<Street>(App.DbContext.Streets);
			AreButtonsEnabled = true;
		}

		private void ButtonAddStreet_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxStreetName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			if (ComboBoxSectors.SelectedItem == null)
			{
				MessageBox.Show("Debe seleccionar un sector", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Street street = new Street()
			{
				Name = name,
				Sector = (Sector)ComboBoxSectors.SelectedItem
			};

			App.DbContext.Streets.Add(street);

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

			MessageBox.Show("Calle agregada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxStreetName.Text = string.Empty;
			ComboBoxSectors.SelectedItem = null;
			Streets.Add(street);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteStreet_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			Street street = (Street)button.DataContext;

			App.DbContext.Streets.Remove(street);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Calle eliminada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Streets.Remove(street);
			AreButtonsEnabled = true;
		}
	}
}
