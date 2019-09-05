using SHC.ViewModels;
using SHC.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SHC
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MainWindowViewModel ViewModel;

		public MainWindow()
		{
			InitializeComponent();
			ViewModel = new MainWindowViewModel();
			DataContext = ViewModel;

			App.MainFrame = FrameContent;

			switch (ViewModel.CheckDatabaseConfiguration())
			{
				case -1:
					FrameContent.Navigate(new InitialConfigPage());
					break;
				case 0:
					App.DbContext = new ShcDbContext(App.SERVER_IP);
					ViewModel.InitializeDatabase();
					FrameContent.Navigate(new HomePage());
					break;
			}
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.GoBack((Page)FrameContent.Content);
		}

		private void FrameContent_Navigated(object sender, NavigationEventArgs e)
		{
			ViewModel.Navigated((Page)FrameContent.Content);
		}
	}
}
