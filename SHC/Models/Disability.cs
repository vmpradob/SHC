using System.Collections.Generic;

namespace SHC.Models
{
	public class Disability
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Patient> Patients { get; set; }

		public Disability()
		{
			Patients = new List<Patient>();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
