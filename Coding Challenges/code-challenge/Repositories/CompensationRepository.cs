using challenge.Data;
using challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }

        public Compensation Add(Compensation employeePay)
        {
            _compensationContext.Compensations.Add(employeePay);
            return employeePay;
        }

        public Compensation GetById(string id)
        {
            //Include does an eager load for the employee
            return _compensationContext.Compensations.Include(e => e.Employee).SingleOrDefault(e => e.Employee.EmployeeId == id);
        }

        public Compensation Remove(Compensation employeePay)
        {
            return _compensationContext.Remove(employeePay).Entity;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
