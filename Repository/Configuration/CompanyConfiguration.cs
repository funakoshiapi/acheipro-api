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
					Name = "Luanda Legal LLC",
					Address = "Golfe 2, Rua 3",
					Country = "Angola",
					Province = "Luanda",
					Industry = "Juridicos",
					Role = "Advogado"
				},

                new Company
                {
                    Id = new Guid("c0f33166-9f5b-11ee-8c90-0242ac120002"),
                    Name = "Contabilistical LLC",
                    Address = "Talatona, Rua 6",
                    Country = "Angola",
                    Province = "Luanda",
					Industry = "Contabilidade",
                    Role = "Contabilidade"
                }
            );
		}
	}
}

