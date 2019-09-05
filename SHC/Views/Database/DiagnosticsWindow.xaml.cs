using SHC.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SHC.Views.Database
{
	/// <summary>
	/// Interaction logic for DiagnosticsWindow.xaml
	/// </summary>
	public partial class DiagnosticsWindow : Window
	{
		public bool AreButtonsEnabled { get; set; }
		public ObservableCollection<Diagnostic> Diagnostics { get; set; }

		public DiagnosticsWindow()
		{
			InitializeComponent();
			DataContext = this;
			Diagnostics = new ObservableCollection<Diagnostic>(App.DbContext.Diagnostics);
			AreButtonsEnabled = true;
		}

		private void ButtonAddDiagnostic_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			string name = TextBoxDiagnosticName.Text;

			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Diagnostic Diagnostic = new Diagnostic()
			{
				Name = name
			};

			App.DbContext.Diagnostics.Add(Diagnostic);

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

			MessageBox.Show("Diagnostico agregada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			TextBoxDiagnosticName.Text = string.Empty;
			Diagnostics.Add(Diagnostic);
			AreButtonsEnabled = true;
		}

		private void ButtonDeleteDiagnostic_Click(object sender, RoutedEventArgs e)
		{
			AreButtonsEnabled = false;
			Button button = ((Button)sender);
			Diagnostic Diagnostic = (Diagnostic)button.DataContext;

			App.DbContext.Diagnostics.Remove(Diagnostic);

			try
			{
				App.DbContext.SaveChanges();
				MessageBox.Show("Diagnostico eliminada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch
			{
				MessageBox.Show("No se pudieron guardar los cambios en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				AreButtonsEnabled = true;
				return;
			}

			Diagnostics.Remove(Diagnostic);
			AreButtonsEnabled = true;
		}
	}
}
