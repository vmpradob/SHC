using System;
using System.Collections.Generic;

namespace SHC.Models
{
	public enum Gender
	{
		Male, Female
	}

	public class Patient
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public DateTime BirthDate { get; set; }
		public Gender Gender { get; set; }
		public string IdCard { get; set; }
		public bool IsARepresentative { get; set; }
		public string PhoneNumber { get; set; }

		public virtual Address Address { get; set; }
		public virtual EducationLevel EducationLevel { get; set; }
		public virtual ICollection<Disability> Disabilities { get; set; }
		public virtual ICollection<Antecedent> Antecedents { get; set; }

		public Patient()
		{
			BirthDate = DateTime.Parse("01/01/1950");
			Address = new Address();
			EducationLevel = new EducationLevel();
			Disabilities = new List<Disability>();
			Antecedents = new List<Antecedent>();
		}

		public override string ToString()
		{
			return string.Format("{0} {1} ({2})", Name, Lastname, IdCard);
		}

		public string Fullname { get => string.Format("{0}, {1}", Lastname, Name); }
		public string GenderToString { get => GetGenderToString(); }
		public int Group { get => GetPatientGroup(); }
		public string GroupToString { get => GetGroupToString(); }

		private string GetGenderToString()
		{
			if (Gender == Gender.Male)
			{
				return "Masculino";
			}
			return "Femenino";
		}

		private int GetPatientGroup()
		{
			var age = DateTime.Now.Year - BirthDate.Year;
			var group = (int)Math.Truncate((double)age / 5);

			if (group > 19)
			{
				return 19;
			}
			return group;
		}

		private string GetGroupToString()
		{
			if (Group < 18)
			{
				return string.Format("De {0} a {1}", Group * 5, (Group * 5) + 4);
			}
			return "De 95 o más";
		}
	}
}
