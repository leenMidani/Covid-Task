using COVID.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace COVID.Domain.Context
{
    public class PatientContext:DbContext
    {
        public PatientContext (DbContextOptions options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientHistory> PatientHistories { get; set; }
        public DbSet <Vacciene> Vacciene { get; set; }
    }
}
