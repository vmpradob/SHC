using SHC.Models;
using System.Data.Entity;
using System.Data.SqlClient;

namespace SHC
{
	public class ShcDbContext : DbContext
	{
		public DbSet<EducationLevel> EducationLevels { get; set; }
		public DbSet<Disability> Disabilities { get; set; }
		public DbSet<Specialty> Specialties { get; set; }

		public DbSet<Street> Streets { get; set; }
		public DbSet<Sector> Sectors { get; set; }
		public DbSet<Community> Communities { get; set; }
		public DbSet<Parish> Parishes { get; set; }
		public DbSet<Address> Addresses { get; set; }

		public DbSet<Antecedent> Antecedents { get; set; }
		public DbSet<Test> Tests { get; set; }

		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Doctor> Doctors { get; set; }	

		public ShcDbContext(string ip) : base(GetRemoteConnectionString(ip))
		{
			Database.SetInitializer(new CreateDatabaseIfNotExists<ShcDbContext>());
			//Database.SetInitializer(new DropCreateDatabaseAlways<ShcDbContext>());
		}

		public static string GetRemoteConnectionString(string ip)
		{
			SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
			{
				DataSource = ip + "," + App.SERVER_PORT,
				InitialCatalog = App.DB_NAME,
				IntegratedSecurity = false,
				MultipleActiveResultSets = true,
				ApplicationName = App.APP_NAME,
				UserID = App.DB_USER,
				Password = App.DB_PASSWORD
			};
			return connectionString.ToString();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Appointment>()
				.Property(x => x.Date)
				.HasColumnType("datetime2");

			modelBuilder.Entity<Patient>()
				.Property(x => x.BirthDate)
				.HasColumnType("datetime2");
		}
	}
}
