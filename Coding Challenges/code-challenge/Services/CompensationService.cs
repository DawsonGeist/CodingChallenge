using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {

        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeService> _logger;

        public CompensationService(ILogger<EmployeeService> logger, ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;
            _employeeService = new EmployeeService(logger, employeeRepository);
            _logger = logger;
        }
        /// <summary>
        /// NOTE C(3): Here we are checking for null values and filling them with default values.
        /// 
        /// on line you XX can see we are syncing Compensation ID with Employee ID and we are using an if statement
        /// to either grad the Employee entity in the employee repository or create a new employee in the repository.
        /// 
        /// This way the ID string in both repositories are the same and I believe, unique 
        /// </summary>
        /// <param name="EmployeePay"></param>
        /// <returns></returns>
        public Compensation Create(Compensation EmployeePay)
        {
            //Check for missing Employee ID
            //Check for null fields
            EmployeePay.EffectiveDate = EmployeePay.EffectiveDate == null ? $"{DateTime.Now}" : EmployeePay.EffectiveDate;
            EmployeePay.Salary = EmployeePay.Salary == 0 ? 1000000 : EmployeePay.Salary;

            //Add the employee entity described in the compensation entity to the employee repository if it does not already exist and update compensation ID
            var employee = _employeeService.GetById(EmployeePay.Employee.EmployeeId);
            if(employee == null)
            {
                EmployeePay.Employee = _employeeService.Create(EmployeePay.Employee);
            }
            else
            {
                EmployeePay.Employee = employee;
            }
            //Sync the Compensation ID with the employee ID
            EmployeePay.CompensationId = _employeeService.Create(EmployeePay.Employee).EmployeeId;

            //Add to the ?Persistance Layer?
            _compensationRepository.Add(EmployeePay);
            _compensationRepository.SaveAsync().Wait();

            return EmployeePay;
        }

        public Compensation Read(string id)
        {
            return !String.IsNullOrEmpty(id) ? _compensationRepository.GetById(id) : null;
        }
    }
}
