using System;
using System.Collections.Generic;

namespace SHC.Models
{
	public class Appointment
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string ReasonForConsultation { get; set; }
		public virtual ICollection<Diagnostic> Diagnostic { get; set; }
		public string Treatment { get; set; }
		public string Observations { get; set; }
	
		public virtual Patient Patient { get; set; }
		public virtual Doctor Doctor { get; set; }
		public virtual ICollection<Test> Tests { get; set; }

		public Appointment()
		{
			Patient = new Patient();
			Doctor = new Doctor();
			Tests = new List<Test>();
		}
	}
}
