using SHC.ViewModels;
using SHC.Views.Statistics;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SHC.Views
{
	/// <summary>
	/// Interaction logic for StatisticsPage.xaml
	/// </summary>
	public partial class StatisticsPage : Page
	{
		StatisticsPageViewModel ViewModel;		

		public StatisticsPage()
		{
			InitializeComponent();
			ViewModel = new StatisticsPageViewModel();
			DataContext = ViewModel;
			FrameStatistic.Navigate(new DiagnosticsPerRegion());
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int uid = int.Parse(((Button)sender).Uid);
			
			var page = (Page)Activator.CreateInstance(ViewModel.Pages[uid]);
			FrameStatistic.Navigate(page);			
		}

		private void FrameStatistic_Navigated(object sender, NavigationEventArgs e)
		{
			ViewModel.Navigated((Page)FrameStatistic.Content);			
		}
	}
}
