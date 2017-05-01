using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_task__1
{
    class RandomString
    {
        // в случайном порядке заполняем столбец коментариев
        Random r = new Random();

        string sumstroka = "";
        int s = 0;
        int z = 0;
        int t = 0;
        public string ArrayRand()
        {
            s = r.Next(0, 6);
            t = r.Next(-1000, 0);
            z = r.Next(0, 10000);

            if (s == 0)
            { sumstroka = "   trade:Sell   result:Profit     " + "   +" + z + " $"; }
            else if (s == 1)
            { sumstroka = "   trade:Sell   result:Loss           " + t + " $"; }
            else if (s == 2)
            { sumstroka = "   trade:Bay    result:Profit     " + "  +" + z + " $"; }
            else if (s == 3)
            { sumstroka = "   trade:Bay    result:Loss          " + t + " $"; }
            else if (s == 4)
            { sumstroka = "   trade:Bay    result:StopLoss  " + t + " $"; }
            else if (s == 5)
            { sumstroka = "   trade:Sell   result:Stoploss  " + t + " $"; }

            else
            { sumstroka = " - "; }

            return sumstroka;
        }
    }
}
