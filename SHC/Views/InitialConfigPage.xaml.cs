using SHC.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for InitialConfigPage.xaml
	/// </summary>
	public partial class InitialConfigPage : Page
	{
		InitialConfigPageViewModel ViewModel;

		public InitialConfigPage()
		{
			InitializeComponent();
			ViewModel = new InitialConfigPageViewModel();
			DataContext = ViewModel;
		}

		private void ButtonConnectDB_Click(object sender, RoutedEventArgs e)
		{
			switch (ViewModel.ConnectToDatabase(TextBoxIp.Text))
			{
				case -3:
					MessageBox.Show("Error al intentar guardar la dirección IP", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case -2:
					MessageBox.Show("Error al intentar conectarse con la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case -1:
					MessageBox.Show("Debe ingresar una dirección IP", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
				case 0:
					MessageBox.Show("Conexión establecida con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
					App.DbContext = new ShcDbContext(App.SERVER_IP);
					App.MainFrame.Navigate(new HomePage());
					break;
			}
		}
	}
}
