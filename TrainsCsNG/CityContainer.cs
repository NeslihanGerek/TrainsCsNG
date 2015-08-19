using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainsCsNG
{
    public class CityContainer
    {
        public Dictionary<char, Int32> _routeList = new Dictionary<char, Int32>();
        private char _cityName; 
        
        public void SetCityName(char cityName)
        {
            _cityName = cityName;
        }

        public char GetCityName()
        {
            return _cityName;
        }

        public void addRoute(char cityName, Int32 route)
        {
            try
            {
                _routeList.Add(cityName, route);
            }
            catch
            {
                Console.WriteLine("a route with city : " + cityName + " already exists");
            }
        }

        public int GetRoute(char cityName)
        {
            var enRoute = _routeList.FirstOrDefault(x => x.Key == cityName).Value;
            return enRoute;
        }
    }
}
