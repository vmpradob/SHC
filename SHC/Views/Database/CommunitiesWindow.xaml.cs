using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for CommunitiesWindow.xaml
	/// </summary>
	public partial class CommunitiesWindow : Window
	{
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<Parish> Parishes { get; set; }
		public ObservableCollection<Community> Communities { get; set; }

		public CommunitiesWindow()
		{
			InitializeComponent();
			DataContext = this;
			Parishes = new ObservableCollection<Parish>(App.DbContext.Parishes);
			Communities = new ObservableCollection<Community>(App.DbContext.Communities);
			AreButtonsEnabled = true;
		}

		private void ButtonAddCommunity_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxCommunityName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			if (ComboBoxParishes.SelectedItem == null)
			{
				MessageBox.Show("Debe seleccionar una parroquía", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Community community = new Community()
			{
				Name = name,
				Parish = (Parish)ComboBoxParishes.SelectedItem
			};

			App.DbContext.Communities.Add(community);			

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

			MessageBox.Show("Comunidad agregada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxCommunityName.Text = string.Empty;
			ComboBoxParishes.SelectedItem = null;
			Communities.Add(community);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteCommunity_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			Community community = (Community)button.DataContext;

			App.DbContext.Communities.Remove(community);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Comunidad eliminada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Communities.Remove(community);
			AreButtonsEnabled = true;
		}
	}
}
