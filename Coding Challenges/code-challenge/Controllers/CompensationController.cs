using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<EmployeeController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation employeePay)
        {
            _logger.LogDebug($"Received compensation create request for '{employeePay?.Employee?.FirstName} {employeePay?.Employee?.LastName}'");
            
            _compensationService.Create(employeePay);

            return Ok(employeePay);
        }

        [HttpGet("{id}", Name = "getCompensationByID")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employeePay = _compensationService.Read(id);

            if (employeePay == null)
                return NotFound();

            return Ok(employeePay);
        }
    }
}
