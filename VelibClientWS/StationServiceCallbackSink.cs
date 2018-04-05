using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VelibClientWS
{
    class StationServiceCallbackSink : ServiceReference3.IStationServiceCallback
    {
        public void Station(int time, string city, [MessageParameter(Name = "station")] string station1, string result)
        {
            Console.WriteLine(city);
            Console.WriteLine(station1);
            Console.WriteLine(result);
        }

        public void StationFinished()
        {
            Console.WriteLine("Station completed");
        }
    }
}
