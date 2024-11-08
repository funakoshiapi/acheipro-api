﻿using System;
using AutoMapper;
using Entities;
using Entities.Models;
using Shared;
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
			CreateMap<CompanyForUpdateDto, Company>().ReverseMap();

			CreateMap<CompanyData, CompanyDataDto>().ReverseMap();
			CreateMap<CompanyDataUpdateDto, CompanyData>().ReverseMap();

            CreateMap<CompanyDataCreationDto, CompanyData>();
			CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
			CreateMap<UserForRegistrationDto, User>();
			CreateMap<CompanyImageDto, CompanyImage>();
			CreateMap<CompanyImage, CompanyImageDto>();
			CreateMap<ClientMessageDto, ClientMessage>();
			CreateMap<PasswordRecoveryDto, PasswordRecovery>();
        }

	}
}

