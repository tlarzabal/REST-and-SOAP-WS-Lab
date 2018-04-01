using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientIHM
{
    public partial class Form1 : Form
    {
        SOAPServer.VelibOperationsClient client;
        string city;

        public Form1()
        {
            InitializeComponent();
            client = new SOAPServer.VelibOperationsClient();
            listCities();
        }

        private async void listStations()
        {
            if (city.Length > 0)
            {
                Task<string[]> asyncResponse = client.getStationsAsync(city);
                string[] stations = await asyncResponse;

                comboBoxStation.Items.Clear();
                foreach (string station in stations)
                    comboBoxStation.Items.Add(station);
            }
        }


        private async void listCities()
        {

                Task<string[]> asyncResponse = client.getCitiesAsync();
                string[] cities = await asyncResponse;
                comboBoxCity.Items.Clear();
                foreach (string city in cities)
                    comboBoxCity.Items.Add(city);
        }


        private async void displayStation(string station)
        {
            Task<string> asyncResponse = client.getAvailableBikesAsync(city, station);
            string text = await asyncResponse;
           
            label4.Text =  text;
        }

        private void comboBoxCity_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            city = comboBoxCity.GetItemText(comboBoxCity.SelectedItem);
            listStations();
        }

        private void comboBoxStation_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string station = comboBoxStation.GetItemText(comboBoxStation.SelectedItem);
            displayStation(station);
        }

    }
}
