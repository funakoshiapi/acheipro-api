using System;
namespace Entities.Exceptions
{
	public class EmployeeNotFoundException : NotFoundException
	{
		public EmployeeNotFoundException(Guid employeeId)
			:base($"Employee wiith id: {employeeId} does not exist in the database.")
		{
		}
	}
}

