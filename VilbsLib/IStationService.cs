using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace VilbsLib
{
    [ServiceContract(CallbackContract = typeof(IStationServiceEvents))]
    interface IStationService
    {
        [OperationContract]
        void getInfos(int time,string city,string station);

        [OperationContract]
        void SubscribeStationEvent();

        [OperationContract]
        void SubscribeStationFinishedEvent();
    }
}
