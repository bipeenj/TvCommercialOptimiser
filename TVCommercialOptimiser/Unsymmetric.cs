using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVCommercialOptimiser
{
    public class Unsymmetric: Symmetric
    {
        public Unsymmetric()
        {
            LastBreakIndex[2]= 3;
            LastBreakIndex[3]= 6;
        }
        public override int GetBreakCount(int brkNo)
        {
            int retCount = 3;
            switch(brkNo)
            {
                case 1:
                    retCount = 2;
                    break;
                case 2:
                    retCount = 3;
                    break;
                case 3:
                    retCount =4;
                    break;
            }
            return retCount;
        }
    }
}
