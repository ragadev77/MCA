using MCA.DBClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MCA.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           //Database.EnsureCreated();
        }


        public DbSet<Rule> Rules { get; set; }
        public DbSet<Parameter_Version> ParameterVersions { get; set; }
        public DbSet<RuleFinal> RuleFinals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parameter_Version>()
                //.ToTable("ParameterVersions", DBSchemas.Version)
                .HasKey(p => p.prv_id);
            modelBuilder.Entity<Parameter_Version>().Property(p => p.prv_date)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            modelBuilder.Entity<Rule>()
                .HasKey(p => p.rul_id);

            modelBuilder.Entity<Rule>().Property(p => p.rul_created)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            modelBuilder.Entity<RuleFinal>()
                //.ToTable("Rules", DBSchemas.Public)
                .HasKey(p => p.rul_id);
            modelBuilder.Entity<RuleFinal>().Property(p => p.rul_created)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            modelBuilder.Entity<RuleFinal>().Property(p => p.rul_is_deleted)
                .HasDefaultValueSql("false")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);


        }
    }
}
