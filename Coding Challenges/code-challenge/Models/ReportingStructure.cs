using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class ReportingStructure
    {
        /// <summary>
        /// This is the head of the reporting structure. The Get request will look to match this ID.
        /// 
        /// When returning the "Fully Qualified" report structure it has to be a nested JSON object...
        /// 
        /// But every employee object has a list of direct reports which is a list of employees... IDK how to format the JSON
        /// 
        /// But thats further down the road.
        /// </summary>
        public Employee Employee;

        public int NumberOfReports;
    }
}
