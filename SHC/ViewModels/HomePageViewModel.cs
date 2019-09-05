using SHC.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SHC.ViewModels
{
	public class HomePageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public List<Type> Pages { get; set; }

		public HomePageViewModel()
		{
			Pages = new List<Type>()
			{
				typeof(AppointmentsPage),
				typeof(PatientsPage),
				typeof(DoctorsPage),
				typeof(StatisticsPage),
				typeof(DatabasePage),
				typeof(AboutPage),
				typeof(ConfigurationPage)
			};

			var patients = App.DbContext.Patients
				.Include("EducationLevel")
				.Include("Address.Parish")
				.Include("Address.Community")
				.Include("Address.Sector")
				.Include("Address.Street")
				.Include("Disabilities")
				.Include("Antecedents");
			var doctors = App.DbContext.Doctors
				.Include("Specialty");
			var appointments = App.DbContext.Appointments
				.Include("Patient")
				.Include("Doctor")
				.Include("Tests");
		}
	}
}
