using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for ParishesWindow.xaml
	/// </summary>
	public partial class ParishesWindow : Window
	{
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<Parish> Parishes { get; set; }

		public ParishesWindow()
		{
			InitializeComponent();
			DataContext = this;
			Parishes = new ObservableCollection<Parish>(App.DbContext.Parishes);
			AreButtonsEnabled = true;
		}

		private void ButtonAddParish_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxParishName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Parish parish = new Parish()
			{
				Name = name
			};

			App.DbContext.Parishes.Add(parish);

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

			MessageBox.Show("Parroquía agregada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxParishName.Text = string.Empty;
			Parishes.Add(parish);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteParish_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			Parish parish = (Parish)button.DataContext;

			App.DbContext.Parishes.Remove(parish);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Parroquía eliminada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Parishes.Remove(parish);
			AreButtonsEnabled = true;
		}
	}
}
