using System;
using System.Collections.Generic;
using System.Linq;

namespace TVCommercialOptimiser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("9 News Commercial structure!");
            Console.WriteLine("Press 0 for 10 commercials,1 for symmetric 9 commercials and 2 for non symmetric 9 commercials:");
            int userInput = -1;
            try
            {
                userInput = int.Parse(Console.ReadLine());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Symmetric obj = null;
            switch (userInput)
            {
                case 0:
                    obj = new _10Commercials();
                    break;
                case 1:
                    obj = new Symmetric();
                    break;
                case 2:
                    obj = new Unsymmetric();
                    break;
            }
            if (obj != null)
            {
                obj.AssignBreaksToCommercials();
                double totalRating = 0;
                var toprint = obj.TotalCommercials.Where(i => i.Index != -1).OrderBy(i => i.Index);
                foreach (var item in toprint)
                {
                    Console.WriteLine(item.ToString());
                    totalRating += item.Rating;
                }
                Console.WriteLine($"Total rating for commercial is = {totalRating}");
            }
            else
                Console.WriteLine("Error in input");
        }
    }
}
