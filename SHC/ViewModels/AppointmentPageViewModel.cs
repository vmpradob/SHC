using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SHC.Models;

namespace SHC.ViewModels
{
	public class AppointmentPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Appointment Appointment { get; set; }
		public Patient Patient { get; set; }
		public bool IsEditEnabled { get; set; }
		public Visibility PatientInformationVisiblity { get; set; }
		public Visibility AppointmentFormVisibility { get; set; }
		public ObservableCollection<Doctor> Doctors { get; set; }
		public ObservableCollection<Disability> PatientDisabilities { get; set; }
		public ObservableCollection<Antecedent> PatientAntecedents { get; set; }
		public ObservableCollection<Test> Tests { get; set; }

		public AppointmentPageViewModel(Appointment appointment)
		{
			if (appointment == null)
			{
				Appointment = new Appointment();
				IsEditEnabled = true;
			}
			else
			{
				Appointment = appointment;
				Patient = App.DbContext.Patients
					.Include("EducationLevel")
					.Include("Address.Parish")
					.Include("Address.Community")
					.Include("Address.Sector")
					.Include("Address.Street")
					.Where(x => x.Id == Appointment.Patient.Id).First();
				IsEditEnabled = false;
			}

			UpdateGuiControls();

			Doctors = new ObservableCollection<Doctor>(App.DbContext.Doctors);
			PatientDisabilities = new ObservableCollection<Disability>(Appointment.Patient.Disabilities);
			PatientAntecedents = new ObservableCollection<Antecedent>(Appointment.Patient.Antecedents);
			Tests = new ObservableCollection<Test>(Appointment.Tests);
		}

		private void UpdateGuiControls()
		{
			if (string.IsNullOrEmpty(Appointment.Patient.IdCard) || string.IsNullOrWhiteSpace(Appointment.Patient.IdCard))
			{
				PatientInformationVisiblity = Visibility.Hidden;
			}
			else
			{
				PatientInformationVisiblity = Visibility.Visible;
			}

			if (string.IsNullOrEmpty(Appointment.Doctor.IdCard) || string.IsNullOrEmpty(Appointment.Patient.IdCard))
			{
				AppointmentFormVisibility = Visibility.Hidden;
			}
			else
			{
				AppointmentFormVisibility = Visibility.Visible;
			}
		}

		internal void SelectDoctor()
		{
			UpdateGuiControls();
		}

		internal int SearchPatient(string patientId, bool? isARepresentative)
		{
			try
			{
				Patient = App.DbContext.Patients
					.Include("EducationLevel")
					.Include("Address.Parish")
					.Include("Address.Community")
					.Include("Address.Sector")
					.Include("Address.Street")
					.Where(x => x.IdCard == patientId && x.IsARepresentative == isARepresentative).First();
			}
			catch
			{

			}

			if (Patient == null)
			{
				return -1;
			}
			else
			{
				Appointment.Patient = Patient;
				PatientDisabilities = new ObservableCollection<Disability>(Appointment.Patient.Disabilities);
				PatientAntecedents = new ObservableCollection<Antecedent>(Appointment.Patient.Antecedents);
				UpdateGuiControls();
				return 0;
			}
		}

		internal int SaveAppointment(string reason, string diagnostic, string treatment, string observations)
		{
			if (string.IsNullOrEmpty(reason) || string.IsNullOrWhiteSpace(reason) ||
				string.IsNullOrEmpty(diagnostic) || string.IsNullOrWhiteSpace(diagnostic) ||
				string.IsNullOrEmpty(treatment) || string.IsNullOrWhiteSpace(treatment) ||
				string.IsNullOrEmpty(observations) || string.IsNullOrWhiteSpace(observations))
			{
				return -1;
			}

			if (Appointment.Doctor == null)
			{
				return -2;
			}

			if (Appointment.Patient == null)
			{
				return -3;
			}

			if (Appointment.Tests.Count == 0)
			{
				return -4;
			}

			Appointment.Date = DateTime.Now;
			App.DbContext.Appointments.Add(Appointment);
			App.DbContext.SaveChanges();
			IsEditEnabled = false;
			return 0;
		}

		internal int AddTest(string testName, string testResult, string testObservation)
		{
			if (string.IsNullOrEmpty(testName) || string.IsNullOrWhiteSpace(testName) ||
				string.IsNullOrEmpty(testResult) || string.IsNullOrWhiteSpace(testResult) ||
				string.IsNullOrEmpty(testObservation) || string.IsNullOrWhiteSpace(testObservation))
			{
				return -1;
			}

			Test test = new Test()
			{
				Name = testName,
				Result = testResult,
				Observations = testObservation
			};

			Tests.Add(test);
			Appointment.Tests.Add(test);
			return 0;
		}

		internal void DeleteTest(Test test)
		{
			if (IsEditEnabled)
			{ 
				Tests.Remove(test);
				Appointment.Tests.Remove(test);
			}
		}
	}
}
