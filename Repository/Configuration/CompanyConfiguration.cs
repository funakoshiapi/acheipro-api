using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.HasData
			(
				new Company
				{
					Id = new Guid("af58eeaa-9f5b-11ee-8c90-0242ac120002"),
					Name = "Luanda Limitada",
					Address = "Golfe 2, Rua 3, Luanda,Angola",
					Country = "Angola",
					Province = "Luanda",
					Industry = "Vendas"
				},

                new Company
                {
                    Id = new Guid("c0f33166-9f5b-11ee-8c90-0242ac120002"),
                    Name = "Solucoes TI ",
                    Address = "Talatona, Rua 6, Luanda,Angola",
                    Country = "Angola",
                    Province = "Luanda",
					Industry = "Tecnologia"
                }
            );
		}
	}
}

