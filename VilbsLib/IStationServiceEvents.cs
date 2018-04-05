using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace VilbsLib
{
    interface IStationServiceEvents
    {

        [OperationContract(IsOneWay = true)]
        void Station(int time, string city, string station,string result);

        [OperationContract(IsOneWay = true)]
        void StationFinished();
    }
}
