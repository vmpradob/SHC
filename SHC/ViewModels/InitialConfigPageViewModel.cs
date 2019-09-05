using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;

namespace SHC.ViewModels
{
	public class InitialConfigPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public bool AreButtonsEnabled { get; set; }

		public InitialConfigPageViewModel()
		{
			AreButtonsEnabled = true;
		}

		public int ConnectToDatabase(string serverIp)
		{
			AreButtonsEnabled = false;
			
			if (string.IsNullOrEmpty(serverIp) || string.IsNullOrWhiteSpace(serverIp))
			{
				AreButtonsEnabled = true;
				return -1;
			}

			try
			{
				using (SqlConnection connection = new SqlConnection(ShcDbContext.GetRemoteConnectionString(serverIp)))
				{
					connection.Open();
				}
			}
			catch
			{
				AreButtonsEnabled = true;
				return -2;
			}

			try
			{
				File.WriteAllText(App.SERVER_IP_FILE_NAME, serverIp);
			}
			catch
			{
				AreButtonsEnabled = true;
				return -3;
			}
				
			App.SERVER_IP = serverIp;
			return 0;			
		}
	}
}
