using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
	public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.HasData
			(
				new Employee
				{
                    Id = new Guid("6f37f3af-7ba0-4365-a98f-924c9a865c8d"),
                    Name = "Felipe Sousa",
                    Position = "Software Developer",
					CompanyId = new Guid("af58eeaa-9f5b-11ee-8c90-0242ac120002")
                },
                new Employee
                {
                    Id = new Guid("7437b1bb-21ac-4d65-aaf5-c689da20b50d"),
                    Name = "Mbula Matadi",
                    Position = "Director Relacoes Publicas",
                    CompanyId = new Guid("c0f33166-9f5b-11ee-8c90-0242ac120002")
                }


            );
		}
	}
}

