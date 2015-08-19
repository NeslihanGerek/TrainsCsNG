using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrainsCsNG
{
    class Program
    {
        static RouteCalculations routeCalculations = new RouteCalculations();

        static void Main(string[] args)
        {
            char[] cities = { 'A', 'E', 'D' };
            int distance = routeCalculations.FixedRouteDistance(cities);
            if (distance > 0)
            {
                Console.WriteLine("Output #1: " + distance);
            }
            else
            {
                Console.WriteLine("NO SUCH ROUTE");
            }

            int numOfRoutes = routeCalculations.GetNumberOfTripsMaxStops('B', 'B', 3);
            Console.WriteLine("Output #6: " + numOfRoutes);

            numOfRoutes = routeCalculations.GetNumberOfTripsExactStops('A', 'C', 4);
            Console.WriteLine("Output #7: " + numOfRoutes);

            int shortestPath = routeCalculations.ShortestRoute('A', 'C');
            Console.WriteLine("Output #8: " + shortestPath);
            shortestPath = routeCalculations.ShortestRoute('B', 'B');
            Console.WriteLine("Output #9: " + shortestPath);

            shortestPath = routeCalculations.DifferentRoutes('C', 'C', 30);
            Console.WriteLine("Output #10: " + shortestPath);
            
            Console.WriteLine("Please press Enter to finish the operation");
            Console.ReadLine();
        }

 
    }
}
