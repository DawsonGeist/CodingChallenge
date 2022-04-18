using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public interface ICompensationService
    {
        Compensation Create(Compensation EmployeePay);
        Compensation Read(string id);
    }
}
