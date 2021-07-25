using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVCommercialOptimiser
{
    public class Commercial
    {
        public string CommercialName { get; set; }
        public string Type { get; set; }

        public string Demographic { get; set; }

        public string BreakName { get; set; } = "";

        public double Rating { get; set; } = 0;

        public int Index { get; set; } = -1;
        public override string ToString()
        {
            return $"{CommercialName}-{Type}-{Demographic}-{BreakName}-{Rating}-{Index}";
        }
    }
}
