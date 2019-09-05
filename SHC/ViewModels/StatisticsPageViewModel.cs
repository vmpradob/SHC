using SHC.Views.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace SHC.ViewModels
{
	public class StatisticsPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public List<Type> Pages { get; set; }
		public string StatisticName { get; set; }

		public StatisticsPageViewModel()
		{
			/*
				1. Diagnósticos más comunes por región
				2. Comunidades con mayor incidencia de X diagnóstico
				3. Diagnósticos más comunes por edad y sexo
				4. Especialidades más requeridas por región
				5. Comunidades con mayor requerimiento de X especialidad
			 */
			Pages = new List<Type>()
			{
				typeof(DiagnosticsPerRegion),
				typeof(CommunitiesPerDiagnostic),
				typeof(DiagnosticsPerGroup),
				typeof(SpecialtiesPerRegion),
				typeof(CommunitiesPerSpecialty)
			};
		}

		public void Navigated(Page page)
		{
			StatisticName = page.Title;
		}
	}
}
