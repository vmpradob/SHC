using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for SectorsWindow.xaml
	/// </summary>
	public partial class SectorsWindow : Window
	{
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<Community> Communities { get; set; }
		public ObservableCollection<Sector> Sectors { get; set; }

		public SectorsWindow()
		{
			InitializeComponent();
			DataContext = this;
			Communities = new ObservableCollection<Community>(App.DbContext.Communities);
			Sectors = new ObservableCollection<Sector>(App.DbContext.Sectors);
			AreButtonsEnabled = true;
		}

		private void ButtonAddSector_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxSectorName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			if (ComboBoxCommunities.SelectedItem == null)
			{
				MessageBox.Show("Debe seleccionar una comunidad", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Sector sector = new Sector()
			{
				Name = name,
				Community = (Community)ComboBoxCommunities.SelectedItem
			};

			App.DbContext.Sectors.Add(sector);

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

			MessageBox.Show("Sector agregado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxSectorName.Text = string.Empty;
			ComboBoxCommunities.SelectedItem = null;
			Sectors.Add(sector);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteSector_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			Sector sector = (Sector)button.DataContext;

			App.DbContext.Sectors.Remove(sector);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Sector eliminado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Sectors.Remove(sector);
			AreButtonsEnabled = true;
		}
	}
}
