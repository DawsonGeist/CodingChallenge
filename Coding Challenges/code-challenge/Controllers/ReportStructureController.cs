using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Controllers
{
    [Route("api/reportstructure")]
    public class ReportStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportStructureService;
        //Note C: Alot of this class was just copy and pasted from Employee service. Im still interested in learning about API development especially with EF Core.
        public ReportStructureController(ILogger<EmployeeController> logger, IReportingStructureService reportStructureService)
        {
            _logger = logger;
            _reportStructureService = reportStructureService;
        }

        [HttpGet("{id}", Name = "getEmployeeReportStructureById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee report structure get request for '{id}'");
            int count = 0;
            var rootEmployee = _reportStructureService.GetReportingStructure(id, ref count);
            var returner = new ReportingStructure {
                NumberOfReports = count,
                Employee = rootEmployee
            };


            if (returner == null)
                return NotFound();

            return Ok(returner);
        }
    }
}
