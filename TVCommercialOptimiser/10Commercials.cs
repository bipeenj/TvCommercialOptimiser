using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVCommercialOptimiser
{
    public class _10Commercials: Symmetric
    {
        public _10Commercials()
        {
            TotalCommercials.Add(new Commercial {
              CommercialName= "Commercial 10",
               Demographic= "T18-40",
                Type  = "Finance"
            });
        }
        protected override bool KeepGoing()
        {
            var notassignedCommercials =  TotalCommercials.Where(i => i.Rating == 0);
            return notassignedCommercials.Count() > 1;
        }
    }
}
