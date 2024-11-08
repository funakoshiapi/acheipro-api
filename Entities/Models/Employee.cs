﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class Employee
	{
        [Column("EmployeeId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string? Position { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}

