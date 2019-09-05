using System.Windows;
using System.Windows.Controls;

namespace SHC
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		// Constants
		public const string SERVER_IP_FILE_NAME = "server_ip.txt";
		public const string SERVER_PORT = "1433";
		public const string APP_NAME = "SHC";
		public const string DB_NAME = "shc_db";
		public const string DB_USER = "shc_admin";
		public const string DB_PASSWORD = "shc_admin";

		// Global properties
		public static string SERVER_IP;
		public static Frame MainFrame;
		public static ShcDbContext DbContext;
	}
}
