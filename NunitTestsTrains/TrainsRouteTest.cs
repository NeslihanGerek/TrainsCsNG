using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainsCsNG;

using NUnit.Framework;

namespace NunitTestsTrains
{
    [TestFixture]
    public class TrainsRouteTest
    {
        public RouteCalculations routeCalculations = new RouteCalculations();

        [Test]
        void fixedRouteTestABC()
        {
            char[] cities = { 'A', 'B', 'C' };
            int distance = routeCalculations.FixedRouteDistance(cities);
            Assert.AreEqual(distance, 9);
        }

        [Test]
        void fixedRouteTestAD()
        {
            char[] cities = { 'A', 'D' };
            int distance = routeCalculations.FixedRouteDistance(cities);
            Assert.AreEqual(distance, 5);
        }

        [Test]
        void fixedRouteTestADC()
        {
            char[] cities = { 'A', 'D', 'C' };
            int distance = routeCalculations.FixedRouteDistance(cities);
            Assert.AreEqual(distance, 13);
        }

        [Test]
        void fixedRouteTestAEBCD()
        {
            char[] cities = { 'A', 'E', 'B', 'C', 'D' };
            int distance = routeCalculations.FixedRouteDistance(cities);
            Assert.AreEqual(distance, 22);
        }

        [Test]
        void fixedRouteTestAED()
        {
            char[] cities = { 'A', 'E', 'D' };
            int distance = routeCalculations.FixedRouteDistance(cities);
            Assert.AreEqual(distance, 0);
        }

        [Test]
        void numOfRouteMaxStopsTestCC()
        {
            int numOfROutes = routeCalculations.GetNumberOfTripsMaxStops('C', 'C', 3);
            Assert.AreEqual(numOfROutes, 2);
        }

        [Test]
        void numOfRouteExactStopsTestAC()
        {
            int numOfROutes = routeCalculations.GetNumberOfTripsMaxStops('A', 'C', 4);
            Assert.AreEqual(numOfROutes, 3);
        }
    
        [Test]
        void shortestPathAC()
        {
            int numOfROutes = routeCalculations.ShortestRoute('A', 'C');
            Assert.AreEqual(numOfROutes, 9);
        }
        
        [Test]
        void shortestPathBB()
        {
            int numOfROutes = routeCalculations.ShortestRoute('B', 'B');
            Assert.AreEqual(numOfROutes, 9);
        }
    }
}
