using System;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
	public class RepositoryContext : IdentityDbContext<User>
	{
		public RepositoryContext(DbContextOptions options)
			: base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new CompanyConfiguration());
			modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }


        public DbSet<Company>? Companies { get; set; }
		public DbSet<Employee>? Employees { get; set; }
		public DbSet<CompanyImage>? CompanyImages { get; set; }
		public DbSet<CompanyData>? CompanyDatas { get; set; }
		public DbSet<ClientMessage> ? ClientMessages { get; set; }

		public DbSet<PasswordRecovery>? PasswordRecoveries { get; set; }

    }
}

