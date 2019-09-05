using SHC.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SHC.ViewModels
{
	public class PatientsPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Patient> Patients { get; set; }
		public int TotalPatients { get; set; }

		public PatientsPageViewModel()
		{			
			Patients = new ObservableCollection<Patient>(App.DbContext.Patients
				.Include("EducationLevel")
				.Include("Address.Parish")
				.Include("Address.Community")
				.Include("Address.Sector")
				.Include("Address.Street")
				.Include("Disabilities")
				.Include("Antecedents"));
			TotalPatients = Patients.Count;
		}
	}
}
