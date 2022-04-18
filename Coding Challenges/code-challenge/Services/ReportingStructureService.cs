using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;
        public ReportingStructureService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        /// <summary>
        /// NOTE B:
        /// 
        /// This function recursively loads and counts every entity in the seed entity's direct report structure
        /// 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns>Employee loaded from the repository</returns>
        public Employee GetReportingStructure(string id, ref int count)
        {
            Employee tempContainer = _employeeRepository.GetById(id);
            if (tempContainer != null)
            {
                if(tempContainer.DirectReports != null)
                {
                    foreach (Employee directReport in tempContainer.DirectReports)
                    {
                        count++;
                        GetReportingStructure(directReport.EmployeeId, ref count);
                    }
                }
            }
            return tempContainer;
        }
    }
}
