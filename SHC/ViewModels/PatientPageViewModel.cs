using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SHC.Models;

namespace SHC.ViewModels
{
	public class PatientPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Patient Patient { get; set; }
		public bool IsEditEnabled { get; set; }
		bool PatientExists;
		public int TotalAppointments { get; set; }
		public string ButtonSaveOrEditContent { get; set; }
		public int SelectedGender { get; set; }
		public ObservableCollection<Parish> Parishes { get; set; }
		public ObservableCollection<Community> Communities { get; set; }
		public ObservableCollection<Sector> Sectors { get; set; }
		public ObservableCollection<Street> Streets { get; set; }
		public ObservableCollection<EducationLevel> EducationLevels { get; set; }
		public ObservableCollection<Disability> PatientDisabilities { get; set; }
		public ObservableCollection<Disability> AvaliableDisabilities { get; set; }
		public ObservableCollection<Antecedent> PatientAntecedents { get; set; }
		public ObservableCollection<Antecedent> AvaliableAntecedents { get; set; }
		public ObservableCollection<Appointment> PatientAppointments { get; set; }

		public PatientPageViewModel(Patient patient)
		{
			if (patient == null)
			{
				Patient = new Patient();				
				IsEditEnabled = true;
				PatientExists = false;
			}
			else
			{
				Patient = patient;				
				IsEditEnabled = false;
				PatientExists = true;
			}

			UpdateGuiControls();

			SelectedGender = (Patient.Gender == Gender.Female) ? 1 : 0;

			Parishes = new ObservableCollection<Parish>(App.DbContext.Parishes);
			EducationLevels = new ObservableCollection<EducationLevel>(App.DbContext.EducationLevels);

			PatientDisabilities = new ObservableCollection<Disability>(Patient.Disabilities);
			try
			{
				var avaliableDisabilities = App.DbContext.Disabilities.Where(x => !Patient.Disabilities.Any(x2 => x2.Id != x.Id));
				AvaliableDisabilities = new ObservableCollection<Disability>(avaliableDisabilities);
			}
			catch
			{
				AvaliableDisabilities = new ObservableCollection<Disability>();
			}

			PatientAntecedents= new ObservableCollection<Antecedent>(Patient.Antecedents);
			try
			{
				var avaliableAntecedents = App.DbContext.Antecedents.Where(x => !Patient.Antecedents.Any(x2 => x2.Id != x.Id));
				AvaliableAntecedents = new ObservableCollection<Antecedent>(avaliableAntecedents);
			}
			catch
			{
				AvaliableAntecedents = new ObservableCollection<Antecedent>();
			}

			var appointments = App.DbContext.Appointments.Where(x => x.Patient.Id == Patient.Id);
			PatientAppointments = new ObservableCollection<Appointment>(appointments);
			TotalAppointments = PatientAppointments.Count;
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

		internal int SaveOrEditPatient()
		{
			if (IsEditEnabled)
			{
				if (string.IsNullOrEmpty(Patient.Name) || string.IsNullOrWhiteSpace(Patient.Name) ||
					string.IsNullOrEmpty(Patient.Lastname) || string.IsNullOrWhiteSpace(Patient.Lastname) ||
					string.IsNullOrEmpty(Patient.IdCard) || string.IsNullOrWhiteSpace(Patient.IdCard) ||
					string.IsNullOrEmpty(Patient.PhoneNumber) || string.IsNullOrWhiteSpace(Patient.PhoneNumber) ||
					string.IsNullOrEmpty(Patient.Address.Community.Name) || string.IsNullOrWhiteSpace(Patient.Address.Community.Name) ||
					string.IsNullOrEmpty(Patient.EducationLevel.Name) || string.IsNullOrWhiteSpace(Patient.EducationLevel.Name))
				{
					return -1;
				}

				Patient.Gender = (Gender)SelectedGender;

				if (PatientExists == false)
				{
					try
					{
						var patient = App.DbContext.Patients.Where(x => x.IdCard == Patient.IdCard).First();
						if (patient != null)
						{
							if (patient.IsARepresentative == Patient.IsARepresentative)
							{
								return -2;
							}
						}
					}
					catch
					{

					}

					App.DbContext.Patients.Add(Patient);
					PatientExists = true;
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

		internal void UpdateParishes()
		{
			Parishes = new ObservableCollection<Parish>(App.DbContext.Parishes);
		}

		internal void UpdateCommunities()
		{
			if (Patient.Address.Parish != null)
			{		
				Communities = new ObservableCollection<Community>(App.DbContext.Communities.Where(x => x.Parish.Id == Patient.Address.Parish.Id));
				UpdateSectors();
			}
			else
			{
				Communities = null;
			}
		}

		internal void UpdateSectors()
		{
			if (Patient.Address.Community != null)
			{
				Sectors = new ObservableCollection<Sector>(App.DbContext.Sectors.Where(x => x.Community.Id == Patient.Address.Community.Id));
				UpdateStreets();
			}
			else
			{
				Sectors = null;
			}
		}

		internal void UpdateStreets()
		{
			if (Patient.Address.Sector != null)
			{
				Streets = new ObservableCollection<Street>(App.DbContext.Streets.Where(x => x.Sector.Id == Patient.Address.Sector.Id));
			}
			else
			{
				Streets = null;
			}
		}

		internal void UpdateEducationsLevels()
		{
			EducationLevels = new ObservableCollection<EducationLevel>(App.DbContext.EducationLevels);
		}			  

		internal void AddDisability(Disability disability)
		{
			PatientDisabilities.Add(disability);
			Patient.Disabilities.Add(disability);
			AvaliableDisabilities.Remove(disability);
		}

		internal void UpdateDisabilities()
		{
			PatientDisabilities = new ObservableCollection<Disability>(Patient.Disabilities);
			try
			{
				var avaliableDisabilities = App.DbContext.Disabilities.Where(x => !Patient.Disabilities.Any(x2 => x2.Id != x.Id));
				AvaliableDisabilities = new ObservableCollection<Disability>(avaliableDisabilities);
			}
			catch
			{
				AvaliableDisabilities = new ObservableCollection<Disability>();
			}
		}

		internal void DeleteDisability(Disability disability)
		{
			PatientDisabilities.Remove(disability);
			Patient.Disabilities.Remove(disability);
			AvaliableDisabilities.Add(disability);
		}

		internal void AddAntecedent(Antecedent antecedent)
		{
			PatientAntecedents.Add(antecedent);
			Patient.Antecedents.Add(antecedent);
			AvaliableAntecedents.Remove(antecedent);
		}

		internal void UpdateAntecedents()
		{
			PatientAntecedents = new ObservableCollection<Antecedent>(Patient.Antecedents);
			try
			{
				var avaliableAntecedents = App.DbContext.Antecedents.Where(x => !Patient.Antecedents.Any(x2 => x2.Id != x.Id));
				AvaliableAntecedents = new ObservableCollection<Antecedent>(avaliableAntecedents);
			}
			catch
			{
				AvaliableAntecedents = new ObservableCollection<Antecedent>();
			}
		}

		internal void DeleteAntecedent(Antecedent antecedent)
		{
			PatientAntecedents.Remove(antecedent);
			Patient.Antecedents.Remove(antecedent);
			AvaliableAntecedents.Add(antecedent);
		}
	}
}
