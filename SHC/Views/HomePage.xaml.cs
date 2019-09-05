using SHC.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
		HomePageViewModel ViewModel;

		public HomePage()
		{
			InitializeComponent();
			ViewModel = new HomePageViewModel();
			DataContext = ViewModel;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int uid = int.Parse(((Button)sender).Uid);

			if (uid == -1)
			{
				Window.GetWindow(this).Close();
			}
			else
			{
				var page = (Page)Activator.CreateInstance(ViewModel.Pages[uid]);
				App.MainFrame.Navigate(page);
			}
		}
	}
}
