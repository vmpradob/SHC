using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SHC.Models;

namespace SHC.ViewModels
{
	public class DoctorPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Doctor Doctor { get; set; }
		public bool IsEditEnabled { get; set; }
		bool DoctorExists;
		public int TotalAppointments { get; set; }
		public string ButtonSaveOrEditContent { get; set; }
		public ObservableCollection<Specialty> Specialties { get; set; }
		public ObservableCollection<Appointment> DoctorAppointments { get; set; }

		public DoctorPageViewModel(Doctor doctor)
		{
			if (doctor == null)
			{
				Doctor = new Doctor();
				IsEditEnabled = true;
				DoctorExists = false;
			}
			else
			{
				Doctor = doctor;
				IsEditEnabled = false;
				DoctorExists = true;
			}

			UpdateGuiControls();

			Specialties = new ObservableCollection<Specialty>(App.DbContext.Specialties);

			var appointments = App.DbContext.Appointments.Where(x => x.Doctor.Id == Doctor.Id);
			DoctorAppointments = new ObservableCollection<Appointment>(appointments);
			TotalAppointments = DoctorAppointments.Count;
		}

		private void UpdateGuiControls()
		{
			if (IsEditEnabled)
			{
				ButtonSaveOrEditContent = "Guardar";
			}
			else
			{
				ButtonSaveOrEditContent = "Editar";
			}
		}

		internal int SaveOrEditDoctor()
		{
			if (IsEditEnabled)
			{
				if (string.IsNullOrEmpty(Doctor.Name) || string.IsNullOrWhiteSpace(Doctor.Name) ||
					string.IsNullOrEmpty(Doctor.Lastname) || string.IsNullOrWhiteSpace(Doctor.Lastname) ||
					string.IsNullOrEmpty(Doctor.IdCard) || string.IsNullOrWhiteSpace(Doctor.IdCard) ||
					string.IsNullOrEmpty(Doctor.PhoneNumber) || string.IsNullOrWhiteSpace(Doctor.PhoneNumber) ||
					string.IsNullOrEmpty(Doctor.Specialty.Name) || string.IsNullOrWhiteSpace(Doctor.Specialty.Name))
				{
					return -1;
				}

				if (DoctorExists == false)
				{
					try
					{
						var doctor = App.DbContext.Doctors.Where(x => x.IdCard == Doctor.IdCard).First();
						if (Doctor != null)
						{
							return -2;
						}
					}
					catch
					{

					}

					App.DbContext.Doctors.Add(Doctor);
					DoctorExists = true;
				}

				App.DbContext.SaveChanges();
			}
			else
			{
				IsEditEnabled = !IsEditEnabled;
				UpdateGuiControls();
				return 0;
			}

			IsEditEnabled = !IsEditEnabled;
			UpdateGuiControls();
			return 1;
		}

		internal void UpdateSpecialties()
		{
			Specialties = new ObservableCollection<Specialty>(App.DbContext.Specialties);
		}
	}
}
