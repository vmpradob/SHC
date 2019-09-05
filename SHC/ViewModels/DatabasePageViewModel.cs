using SHC.Views.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SHC.ViewModels
{
	public class DatabasePageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public List<Type> Windows { get; set; }

		public DatabasePageViewModel()
		{
			Windows = new List<Type>()
			{
				typeof(EducationLevelsWindow),
				typeof(DisabilitiesWindow),
				typeof(AntecedentsWindow),
				typeof(ParishesWindow),
				typeof(CommunitiesWindow),
				typeof(SectorsWindow),
				typeof(StreetsWindow),
				typeof(SpecialtiesWindow),
				typeof(DiagnosticsWindow)
			};
		}
	}
}
