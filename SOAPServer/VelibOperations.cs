using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace SOAPServer
{
    public class VelibOperations : IVelibOperations
    {
        private static readonly string APIKey = "adbe62f42efa11f8b9ad21702f83607cf457300d";
        private Dictionary<string, string> cache = new Dictionary<string, string>();

        public string getAvailableBikes(string city,string station)
        {
            string cityJson = getJson(city);

            JArray json = JArray.Parse(cityJson);

            bool cb = false;
            bool noStation = true;
            string result=""; 
            foreach (var child in json)
            {
                    string name = (string)child["name"];


                    if (name.Contains(station))
                    {
                    noStation = false;
                        cb = (bool)child["banking"];

                    result =("**********  " + name + "  **********");
                    result += ("Localisation : " + (string)child["address"] + ", " + (string)child["contract_name"]);
                    result +=("Statut de la station : " + (string)child["status"]);
                    result +=("Vélos (dispo/Total) : " + (string)child["available_bikes"] + "/" + (string)child["bike_stands"]);
                    result += ("Emplacements dispo : " + (int)child["available_bike_stands"]);

                        if (cb)
                        {
                        result +=("Paiement par CB autorisé");
                        }
                        else
                        {
                        result +=("Paiement par CB impossible");
                        }
                    }

                }

                if (noStation)
                {
                result ="Aucune station détectée...";
                }
            return result;
        }

        public async Task<string> getAvailableBikesAsync(string city, string station)
        {
            Task<string> getJson = getJsonAsync(city);
            string cityJson = await getJson;

            JArray json = JArray.Parse(cityJson);


            bool cb = false;
            bool noStation = true;
            string result = "";
            foreach (var child in json)
            {
                string name = (string)child["name"];


                if (name.Contains(station))
                {
                    cb = (bool)child["banking"];

                    result = ("**********  " + name + "  **********");
                    result += ("Localisation : " + (string)child["address"] + ", " + (string)child["contract_name"]);
                    result += ("Statut de la station : " + (string)child["status"]);
                    result += ("Vélos (dispo/Total) : " + (string)child["available_bikes"] + "/" + (string)child["bike_stands"]);
                    result += ("Emplacements dispo : " + (int)child["available_bike_stands"]);

                    if (cb)
                    {
                        result += ("Paiement par CB autorisé");
                    }
                    else
                    {
                        result += ("Paiement par CB impossible");
                    }
                }

            }

            if (noStation)
            {
                result = "Aucune station détectée...";
            }
            return result;
        }

        public IList<string> getStations(string city)
        {
            IList<string> result = new List<string>();
            string cityJson = getJson(city);

            JArray json = JArray.Parse(cityJson);

            foreach (var child in json)
            {
                result.Add((string)child["name"]);
            }

            return result;
        }

        public async Task<IList<string>> getStationsAsync(string city)
        {
            IList<string> result = new List<string>();
            Task<string> getJson = getJsonAsync(city);
            string cityJson = await getJson;

            JArray json = JArray.Parse(cityJson);

            foreach (var child in json)
            {
                result.Add((string)child["name"]);
            }

            return result;
        }








        public IList<string> getCities()
        {
            IList<string> result = new List<string>();
            string cityJson = getJsonCities();

            JArray json = JArray.Parse(cityJson);

            foreach (var child in json)
            {
                result.Add((string)child["name"]);
            }

            return result;
        }

        public async Task<IList<string>> getCitiesAsync()
        {
            IList<string> result = new List<string>();
            Task<string> getJson = getJsonCitiesAsync();
            string cityJson = await getJson;

            JArray json = JArray.Parse(cityJson);

            foreach (var child in json)
            {
                result.Add((string)child["name"]);
            }

            return result;
        }


        private string getJsonCities()
        {
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + APIKey);
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                return responseFromServer;

        }

        private Task<string> getJsonCitiesAsync()
        {
            return Task<string>.Run(() => {
                return getJsonCities();
            });
        }

        private string getJson(string city)
        {
            if (cache.ContainsKey(city))
            {
                return cache[city];
            }
            else
            {
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=" + APIKey);
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                cache.Add(city, responseFromServer);

                return responseFromServer;
            }
        }

        private Task<string> getJsonAsync(string city)
        {
            return Task<string>.Run(() => {
                return getJson(city);
            });
        }
    }
}
