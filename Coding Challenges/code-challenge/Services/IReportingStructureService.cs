using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public interface IReportingStructureService
    {
        Employee GetReportingStructure(string id, ref int count);
    }
}
