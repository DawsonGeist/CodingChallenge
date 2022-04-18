using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                //NOTE
                if (employee.DirectReports != null)
                {
                    List<Employee> newReportList = new List<Employee>();
                    foreach (Employee possibleDuplicate in employee.DirectReports)
                    {
                        //There was an error happening because I was trying to have an existing entity as a direct report
                        //But it was creating a new entity with the same ID which lead to an exception when trying to save the context?
                        //Easy fix, just check for the id in the current repository and if it DNE then just add the new employee object to the list 
                        //and the database will handle it fine.. maybe
                        newReportList.Add(_employeeRepository.GetById(possibleDuplicate.EmployeeId) ?? _employeeRepository.Add(possibleDuplicate));
                    }
                    employee.DirectReports = newReportList;
                }
                //End edits
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            //NOTE
            return !String.IsNullOrEmpty(id) ? _employeeRepository.GetById(id) : null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }
    }
}
