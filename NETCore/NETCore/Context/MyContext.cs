using Microsoft.EntityFrameworkCore;
using NETCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts{ get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(a => a.Account)
                .WithOne(p => p.Person)
                .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(pr => pr.Profiling)
                .WithOne(a => a.Account)
                .HasForeignKey<Profiling>(pr => pr.NIK);

            /*modelBuilder.Entity<AccountRole>()
                .HasKey(ar => new { ar.NIK, ar.RoleId});
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(b => b.AccountRoles)
                .HasForeignKey(ar => ar.NIK);
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.AccountRoles)
                .HasForeignKey(bc => bc.RoleId);*/

            modelBuilder.Entity<Role>().HasMany(r => r.AccountRoles).WithOne(r => r.Role); 
            modelBuilder.Entity<Account>().HasMany(a => a.AccountRoles).WithOne(a => a.Account); 
            modelBuilder.Entity<AccountRole>().HasKey(ar => new { ar.NIK, ar.RoleId });

            modelBuilder.Entity<Education>()
                .HasMany(pr => pr.Profilings)
                .WithOne(e => e.Education);

            modelBuilder.Entity<University>()
                .HasMany(e => e.Educations)
                .WithOne(u => u.University);
        }
    }
}
