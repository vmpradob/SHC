using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for AntecedentsWindow.xaml
	/// </summary>
	public partial class AntecedentsWindow : Window
	{
		public string[] AntecedentTypes { get; set; } = { "Personal", "Familiar" };
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<Antecedent> Antecedents { get; set; }

		public AntecedentsWindow()
		{
			InitializeComponent();
			DataContext = this;
			Antecedents = new ObservableCollection<Antecedent>(App.DbContext.Antecedents);
			AreButtonsEnabled = true;
		}

		private void ButtonAddAntecedent_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxAntecedentName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre para el antecedente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			if (ComboBoxAntecedentTypes.SelectedItem == null)
			{
				MessageBox.Show("Debe seleccionar un tipo de antecedente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Antecedent antecedent = new Antecedent()
			{
				Name = name,
				Type = (AntecedentType)ComboBoxAntecedentTypes.SelectedIndex
			};

			App.DbContext.Antecedents.Add(antecedent);

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
			
			MessageBox.Show("Antecedente agregado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxAntecedentName.Text = string.Empty;
			ComboBoxAntecedentTypes.SelectedItem = null;
			Antecedents.Add(antecedent);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteAntecedent_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			Antecedent antecedent = (Antecedent)button.DataContext;

			App.DbContext.Antecedents.Remove(antecedent);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Antecedente eliminado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Antecedents.Remove(antecedent);
			AreButtonsEnabled = true;
		}
	}
}
