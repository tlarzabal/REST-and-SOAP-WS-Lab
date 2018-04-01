using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static SOAPServer.VelibOperationsClient client;

        private static bool exitBool;
        static void Main(string[] args)
        {
            client = new SOAPServer.VelibOperationsClient();
            help();
            exitBool = true;

            while (exitBool)
            {
                string station_recherchee = null;
                while (station_recherchee == null || station_recherchee == "")
                {
                    Console.WriteLine("\n" + "Entrez le nom partiel/complet ou l'id d'une station");
                    station_recherchee = Console.ReadLine();
                    station_recherchee = station_recherchee.ToUpper();

                }
                switch (station_recherchee)
                {
                    case "--HELP":
                        help();
                        break;
                    case "--EXIT":
                        exit();
                        break;
                    case "--CITIES":
                        cities();
                        break;
                    default:
                        if (station_recherchee.Split(' ')[0].Contains("--LIST"))
                        {
                            list(station_recherchee.Split(' ')[1]);
                        }
                        else if(station_recherchee.Split(' ').Length >= 3)
                            findStation(station_recherchee.Split(' ')[1], station_recherchee.Split(' ')[2]);
                        break;

                }


            }
        }

        static void afficherStations(string ville)
        {
            foreach (string station in client.getStations(ville))
            {
                Console.WriteLine(station);
            }
            Console.WriteLine();
        }

        static void afficherVelibsDispos(string ville, string station)
        {
            Console.WriteLine("Il reste " + client.getAvailableBikes(ville, station) + " velibs dispos à cette station.\n");
        }


        private static void help()
        {
            Console.WriteLine("\n" + "********* HELP  ******** \n --help : Pour afficher l'aide \n --station city stationName : Pour afficher les informations d'une station \n --cities : Pour afficher la liste des villes disponibles \n --list city : Pour afficher la liste des stations disponibles dans la ville \n --exit : Pour quitter.\n");
        }

        private static void exit()
        {
            exitBool = false;
        }


        private static void list(string city)
        {
            Console.WriteLine("********** " + city + " **********");
            foreach (string station in client.getStations(city))
            {
               Console.WriteLine(station);
            }
            Console.WriteLine("\n");
        }


        private static void cities()
        {
            foreach (string city in client.getCities())
            {
               Console.WriteLine(city);
            }
            Console.WriteLine("\n");
        }

        private static void findStation(string city,string station_recherchee)
        {
            Console.WriteLine(client.getAvailableBikes(city,station_recherchee));
        }
    
    }
}
