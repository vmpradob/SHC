using SHC.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for DatabasePage.xaml
	/// </summary>
	public partial class DatabasePage : Page
	{
		DatabasePageViewModel ViewModel;

		public DatabasePage()
		{
			InitializeComponent();
			ViewModel = new DatabasePageViewModel();
			DataContext = ViewModel;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int uid = int.Parse(((Button)sender).Uid);			
			var window = (Window)Activator.CreateInstance(ViewModel.Windows[uid]);
			window.ShowDialog();
		}
	}
}
