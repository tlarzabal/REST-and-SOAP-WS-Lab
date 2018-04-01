using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SOAPServer
{
    [ServiceContract]
    public interface IVelibOperations
    {
        [OperationContract]
        IList<string> getStations(string city);

        [OperationContract]
        IList<string> getCities();

        [OperationContract]
        string getAvailableBikes(string city, string station);
    }
}
