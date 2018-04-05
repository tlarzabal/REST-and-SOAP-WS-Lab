using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace VelibClientWS
{
    class Program
    {
        static void Main(string[] args)
        {
            StationServiceCallbackSink objsink = new StationServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);
 
                ServiceReference3.StationServiceClient objClient = new ServiceReference3.StationServiceClient(iCntxt);
                objClient.SubscribeStationEvent();
                objClient.SubscribeStationFinishedEvent();
                objClient.getInfos(5000, "TOULOUSE", "FEUGA");

            Console.WriteLine("Press any key to close ...");
            Console.ReadKey();
        }
    }
}
