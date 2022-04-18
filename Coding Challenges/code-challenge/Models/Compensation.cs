using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        //NOTE C(2): Defeat
        public String CompensationId { get; set; }

        public Employee Employee { get; set; }

        public System.Int32 Salary { get; set; }

        public String EffectiveDate { get; set; }
    }
}
