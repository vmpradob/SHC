namespace SHC.Models
{
	public class Doctor
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public string IdCard { get; set; }
		public string PhoneNumber { get; set; }

		public virtual Specialty Specialty { get; set; }

		public Doctor()
		{
			Specialty = new Specialty();
		}

		public override string ToString()
		{
			return string.Format("{0} {1} ({2})", Name, Lastname, IdCard);
		}
	}
}
