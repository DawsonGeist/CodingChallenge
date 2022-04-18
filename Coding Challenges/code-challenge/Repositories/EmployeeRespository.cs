using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        /// <summary>
        /// Return the employee from the employee context whom ID matches the input
        /// 
        /// NOTE A: https://stackoverflow.com/questions/42596608/no-nested-results-in-entity-framework-core
        /// 
        /// This was a change needed because DirectReports was null if I didnt inspect direct reports in the debugger.
        /// Using the include function does an "EagerLoad" which loads the first entity as well as any related entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetById(string id)
        {
            //Include does an eager load for the direct reports
            return _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
