using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrainsCsNG
{
    public class RouteCalculations
    {
        public List<CityContainer> _cityContainerList = new List<CityContainer>();
        int _maxStops = 0;
        int _noOfTrips = 0;

        public RouteCalculations()
        {
            try
            {
                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    String line = sr.ReadToEnd();
                    string[] routes = Regex.Split(line, ", ");
                    foreach(string route in routes)
                    {
                        CityContainer cityContainer = AddOrReturnCityContainer(route[0]);
                        cityContainer.addRoute(route[1], (route[2] -'0'));
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public CityContainer AddOrReturnCityContainer(char cityName)
        {
            CityContainer result = _cityContainerList.Find(x => x.GetCityName() == cityName);

            if (null == result)
            {
                result = new CityContainer();
                result.SetCityName(cityName);
                _cityContainerList.Add(result);
            }

            return result;
        }

        public int FixedRouteDistance(params char[] cities)
        {
            int distance = 0;
            char prevCity = '0';
            foreach (char city in cities)
            {
                if (prevCity == '0')
                {
                    prevCity = city;
                    continue;
                }

                CityContainer result = _cityContainerList.Find(x => x.GetCityName() == prevCity);
                if (null != result && result.GetRoute(city) > 0)
                {
                    distance += result.GetRoute(city);
                }
                else
                {
                    distance = 0; // NO SUCH ROUTE
                    break;
                }
                prevCity = city;
            }

            return distance;
        }

        public void GetNumberOfTrips(char startingCity, char endingCity, int maxStops, bool exact)
        {
            if(startingCity == endingCity)
            {
                if ((exact && maxStops == _maxStops) || (!exact && maxStops <= _maxStops))
                {
                    _noOfTrips++;
                    return;
                }
            }

            else if (maxStops >= _maxStops)
            {
                return;
            }
            CityContainer startContainer = _cityContainerList.Find(x => x.GetCityName() == startingCity);

            foreach (KeyValuePair<char, Int32> entry in startContainer._routeList)
            {
                GetNumberOfTrips(entry.Key, endingCity, maxStops+1, exact);
            }
        }

        public int GetNumberOfTripsExactStops(char startingCity, char endingCity, int exactStops)
        {
            _maxStops = exactStops;
            _noOfTrips = 0;
            CityContainer startContainer = _cityContainerList.Find(x => x.GetCityName() == startingCity);

            foreach (KeyValuePair<char, Int32> entry in startContainer._routeList)
            {
                GetNumberOfTrips(entry.Key, endingCity, 1, true);
            }

            return _noOfTrips;
        }

        public int GetNumberOfTripsMaxStops(char startingCity, char endingCity, int maxStops)
        {
            _maxStops = maxStops;
            _noOfTrips = 0;
            CityContainer startContainer = _cityContainerList.Find(x => x.GetCityName() == startingCity);

            foreach (KeyValuePair<char, Int32> entry in startContainer._routeList)
            {
                GetNumberOfTrips(entry.Key, endingCity, 1, false);
            }

            return _noOfTrips;
        }
        
        public int ShortestRoute(char startingCity, char endingCity)
        {
            int route = 0;
            CityContainer startContainer = _cityContainerList.Find(x => x.GetCityName() == startingCity);
            foreach (KeyValuePair<char, Int32> entry in startContainer._routeList)
            {
                List<char> path = new List<char>();
                List<int> pathLength = new List<int>();
                path.Add(startingCity);
                path.Add(entry.Key);
                pathLength.Add(entry.Value);
                GetPathLength(entry.Key, endingCity, ref pathLength, ref path);
                if (route == 0)
                {
                    route = pathLength.Sum();
                }
                else if(pathLength.Count > 0)
                {
                    route = Math.Min(route, pathLength.Sum());
                }
            }

            return route;
        }

        public void GetPathLength(char startingCity, char endingCity, ref List<int> pathLength, ref List<char> path)
        {
            if (startingCity == endingCity)
            {
                return;
            }

            CityContainer startContainer = _cityContainerList.Find(x => x.GetCityName() == startingCity);


            foreach (KeyValuePair<char, Int32> entry in startContainer._routeList)
            {
                if (path.FindAll(x => x == startingCity).Count > 1) // in a loop
                {
                    pathLength.RemoveAt(pathLength.Count -1);
                    path.RemoveAt(path.Count - 1);
                    return;
                }

                while(pathLength.Count >= path.Count)
                {
                    pathLength.RemoveAt(pathLength.Count - 1);
                }
                path.Add(entry.Key);
                pathLength.Add(entry.Value);
                GetPathLength(entry.Key, endingCity, ref pathLength, ref path);
            }

            for(int i=0; i<startContainer._routeList.Count; i++)
            {
                path.RemoveAt(path.Count - 1);
            }
        }

        public void GetPathLengthLimited(char startingCity, char endingCity,int maxDistance, ref List<int> pathLength, ref List<char> path, ref int counter)
        {
            if (pathLength.Sum() >= maxDistance)
            {
                return;
            }
            else if(startingCity == endingCity) // increment counter but continue search
            {
                counter++;
            }

            CityContainer startContainer = _cityContainerList.Find(x => x.GetCityName() == startingCity);


            foreach (KeyValuePair<char, Int32> entry in startContainer._routeList)
            {
                while (pathLength.Count >= path.Count)
                {
                    pathLength.RemoveAt(pathLength.Count - 1);
                }
                path.Add(entry.Key);
                pathLength.Add(entry.Value);
                GetPathLengthLimited(entry.Key, endingCity, maxDistance, ref pathLength, ref path, ref counter);
                path.RemoveAt(path.Count - 1);
            }
        }

        public int DifferentRoutes(char startingCity, char endingCity, int maxDistance)
        {
            int route = 0;
            CityContainer startContainer = _cityContainerList.Find(x => x.GetCityName() == startingCity);
            foreach (KeyValuePair<char, Int32> entry in startContainer._routeList)
            {
                List<char> path = new List<char>();
                List<int> pathLength = new List<int>();
                path.Add(startingCity);
                path.Add(entry.Key);
                pathLength.Add(entry.Value);
                GetPathLengthLimited(entry.Key, endingCity, maxDistance, ref pathLength, ref path, ref route);
            }

            return route;
        }

    }
}
