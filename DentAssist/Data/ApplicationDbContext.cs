using Microsoft.EntityFrameworkCore;
using DentAssist.Models;

namespace DentAssist.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Odontologo> Odontologos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }
        public DbSet<PlanTratamiento> PlanesTratamiento { get; set; }
        public DbSet<PasoTratamiento> PasosTratamiento { get; set; }
    }
}
