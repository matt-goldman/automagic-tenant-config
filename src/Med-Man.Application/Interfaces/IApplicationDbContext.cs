using MedMan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedMan.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Administration> Administrations { get; set; }
    }
}
