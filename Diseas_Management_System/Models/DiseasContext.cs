using Microsoft.EntityFrameworkCore;

namespace Diseas_Management_System.Models
{
    public class DiseasContext : DbContext
    {
        public DiseasContext(DbContextOptions<DiseasContext> options) : base(options) { }

        public DbSet<Diseas> Diseases { get; set; } = null!;

        public DbSet<BodyPart> BodyParts { get; set; } = null!;

        public DbSet<DiseasBodyPart> DiseasBodyParts { get; set; } = null!;

        public DbSet<DiseasMedicine> DiseasMedicines { get; set; } = null!;

        public DbSet<DiseasReport> DiseasReports { get; set; } = null!;

        public DbSet<Medicine> Medicines { get; set; } = null!;

        public DbSet<Report> Reports { get; set; } = null!;

        public DbSet<Admin> Admin { get; set; } = null!;
    }
}
