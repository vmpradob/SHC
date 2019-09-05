using System.Collections.Generic;

namespace SHC.Models
{
	public enum AntecedentType
	{
		Personal, Familiar
	}

	public class Antecedent
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public AntecedentType Type { get; set; }

		public virtual ICollection<Patient> Patients { get; set; }

		public Antecedent()
		{
			Patients = new List<Patient>();
		}

		public override string ToString()
		{
			return Name;
		}

		public string AntecedentTypeToString { get => AntecedentTypeToStringMethod(); }
		public string AntecedentTypeToStringMethod()
		{
			if (Type == AntecedentType.Personal)
			{
				return "Personal";
			}
			return "Familiar";
		}
	}
}
