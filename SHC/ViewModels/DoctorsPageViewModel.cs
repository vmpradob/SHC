using SHC.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SHC.ViewModels
{
	public class DoctorsPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Doctor> Doctors { get; set; }
		public int TotalDoctors { get; set; }

		public DoctorsPageViewModel()
		{
			Doctors = new ObservableCollection<Doctor>(App.DbContext.Doctors
				.Include("Specialty"));
			TotalDoctors = Doctors.Count;
		}
	}
}
