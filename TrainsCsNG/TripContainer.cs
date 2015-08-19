using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainsCsNG
{
    class TripContainer
    {
        private char _startingCity;
        private char _endingCity;
        public List<RouteContainer> _routeContainerList = new List<RouteContainer>();

        public TripContainer(char startingCity, char endingCity)
        {
            _startingCity = startingCity;
            _endingCity = endingCity;
            
        }

        public void addRoute(string route, Int32 distance, Int32 numStops)
        {
            _routeContainerList.Add(new RouteContainer(route, distance, numStops));
        }

        public void GetRoute()
        {

        }
    }

    class RouteContainer
    {
        public string _route;
        public Int32 _distance;
        public Int32 _numStops;
        
        public RouteContainer(string route, Int32 distance, Int32 numStops)
        {
            _route = route;
            _distance = distance;
            _numStops = numStops;
        }
    }
}
