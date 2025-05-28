
using FirstTestAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FirstTestAPI.Contexts
{
    public class ClinicContext :DbContext
    {

        public ClinicContext(DbContextOptions options) : base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestDatabase;Username=postgres;Password=admin;");
        //}

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<DoctorSpeciality> DoctorSpecialities   { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasKey(app => app.Id).HasName("Pk_AppointmentId");

            modelBuilder.Entity<Appointment>().HasOne(app=>app.Patient)
                                            .WithMany(p=>p.Appointments)
                                            .HasForeignKey(app=>app.Id)
                                            .HasConstraintName("Fk_Appointment_Patient")
                                            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>().HasOne(app => app.Doctor)
                                                .WithMany(d => d.Appointments)
                                                .HasForeignKey(app => app.Id)
                                                .HasConstraintName("FK_Appointment_Doctor")
                                                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorSpeciality>().HasKey(ds => ds.SerialNumber);

            modelBuilder.Entity<DoctorSpeciality>().HasOne(ds=>ds.Doctor)
                                                    .WithMany(d=>d.DoctorSpecialities)
                                                    .HasForeignKey(ds=>ds.DoctorId)
                                                    .HasConstraintName("FK_Speciality_Doctor")
                                                    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DoctorSpeciality>().HasOne(ds => ds.Speciality)
                                                    .WithMany(d => d.DoctorSpecialities)
                                                    .HasForeignKey(ds => ds.SpecialityId)
                                                    .HasConstraintName("FK_Speciality_Spec")
                                                    .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
