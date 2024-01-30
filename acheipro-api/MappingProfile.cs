using System;
using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace acheipro_api
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Company, CompanyDto>()
				.ForCtorParam("FullAddress",
					opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Province, x.Country)));

			CreateMap<CompanyCreationDto, Company>();
			CreateMap<CompanyForUpdateDto, Company>();

			CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
			CreateMap<UserForRegistrationDto, User>();
        }

	}
}

