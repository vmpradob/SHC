using SHC.Models;
using SHC.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SHC.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Visibility ButtonBackVisibility { get; set; }
		public string PageTitle { get; set; }

		public MainWindowViewModel()
		{
			ButtonBackVisibility = Visibility.Hidden;
			PageTitle = string.Empty;
		}
		
		public int CheckDatabaseConfiguration()
		{
			try
			{
				string serverIp = File.ReadAllText(App.SERVER_IP_FILE_NAME);

				if (string.IsNullOrEmpty(serverIp) || string.IsNullOrEmpty(serverIp))
				{
					return -1;
				}
				else
				{
					App.SERVER_IP = serverIp;					
					return 0;
				}
			}
			catch
			{
				return -1;
			}
		}

		public void InitializeDatabase()
		{
			var specialties = new List<Specialty>(App.DbContext.Specialties);

			if (specialties != null)
			{
				if (specialties.Count == 0)
				{
					App.DbContext.Specialties.Add(new Specialty()
					{
						Name = "Medicina Interna"
					});

					App.DbContext.Specialties.Add(new Specialty()
					{
						Name = "Pediatría"
					});

					App.DbContext.Specialties.Add(new Specialty()
					{
						Name = "Mastología"
					});

					App.DbContext.Specialties.Add(new Specialty()
					{
						Name = "Ginecología"
					});

					App.DbContext.SaveChanges();
				}
			}
		}

		public void GoBack(Page page)
		{
			if (page.GetType() == typeof(StatisticsPage))
			{
				App.MainFrame.Navigate(new HomePage());
			}
			else
			{
				App.MainFrame.GoBack();
			}
		}

		public void Navigated(Page page)
		{
			if (page.GetType() == typeof(InitialConfigPage) || page.GetType() == typeof(HomePage))
			{
				ButtonBackVisibility = Visibility.Hidden;
			}
			else
			{
				ButtonBackVisibility = Visibility.Visible;
			}

			PageTitle = page.Title;
		}
	}
}
