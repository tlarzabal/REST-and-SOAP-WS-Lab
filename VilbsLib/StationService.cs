using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace VilbsLib
{


    public class StationService : IStationService
    {

        private static readonly string APIKey = "adbe62f42efa11f8b9ad21702f83607cf457300d";
        static Action<int, string,string, string> m_Event1 = delegate { };
        static Action m_Event2 = delegate { };

        public void getInfos(int time,string city ,string station)
        {
            string result = getAvailableBikes(city, station);
            m_Event1(time,city, station, result);
            m_Event2();

        }

        public void SubscribeStationEvent()
        {
            IStationServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IStationServiceEvents>();
            m_Event1 += subscriber.Station;
        }

        public void SubscribeStationFinishedEvent()
        {
            IStationServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IStationServiceEvents>();
            m_Event2 += subscriber.StationFinished;
        }


        private string getJson(string city)
        {
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=" + APIKey);
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                return responseFromServer;
         
        }


        private string getAvailableBikes(string city, string station)
        {
            string cityJson = getJson(city);

            JArray json = JArray.Parse(cityJson);

            bool cb = false;
            bool noStation = true;
            string result = "";
            foreach (var child in json)
            {
                string name = (string)child["name"];


                if (name.Contains(station))
                {
                    noStation = false;
                    cb = (bool)child["banking"];

                    result = ("**********  " + name + "  **********");
                    result += ("\n Localisation : " + (string)child["address"] + ", " + (string)child["contract_name"]);
                    result += ("\n Statut de la station : " + (string)child["status"]);
                    result += ("\n Vélos (dispo/Total) : " + (string)child["available_bikes"] + "/" + (string)child["bike_stands"]);
                    result += ("\n Emplacements dispo : " + (int)child["available_bike_stands"]);

                    if (cb)
                    {
                        result += ("\n Paiement par CB autorisé");
                    }
                    else
                    {
                        result += ("\n Paiement par CB impossible");
                    }
                }

            }

            if (noStation)
            {
                result = "Aucune station détectée...";
            }
            return result;
        }
    }
}
