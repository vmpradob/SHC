using SHC.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SHC.ViewModels
{
	public class AppointmentsPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Appointment> Appointments { get; set; }
		public int TotalAppointments { get; set; }

		public AppointmentsPageViewModel()
		{
			Appointments = new ObservableCollection<Appointment>(App.DbContext.Appointments
				.Include("Patient")
				.Include("Doctor")
				.Include("Tests"));
			TotalAppointments = Appointments.Count;
		}
	}
}
