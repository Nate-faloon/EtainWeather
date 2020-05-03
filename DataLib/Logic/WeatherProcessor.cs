using DataLib.DataHandlers;
using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Logic
{
    public class WeatherProcessor
    {
        public DateTime LastSync { get; set; }

        public async Task<List<WeatherDataModel>> LoadWeather(DateTime DateWeather)
        {
            ApiHandler.InitializeApi(); //initialise our http handler

            List<WeatherDataModel> weatherMain = new List<WeatherDataModel>(); //make a list of type model to store the first result from each api call



            for (int i = 0; i < 6; i++) // 0-6 gets todays weather plus 5 days
            {
                //adding 'i' days to make the call dynamic
                string url = $" {ApiHandler.ApiClient.BaseAddress + DateWeather.Date.AddDays(i).ToString("yyyy/MM/dd") }/"; //this will be appended to our base url to get the weather for selected date

                using (HttpResponseMessage response = await ApiHandler.ApiClient.GetAsync(url)) //using statement handles the releasing of variables before next loop
                {
                    if (response.IsSuccessStatusCode) //if page gives back status 200 (all ok) 
                    {
                        // we have succesful call here
                        List<WeatherDataModel> weather = await response.Content.ReadAsAsync<List<WeatherDataModel>>();

                        weatherMain.Add(weather[0]); //this allows me to only get the most recent result from the weather API

                        LastSync = DateTime.Now; //updates last sync time
                    }
                }
            }


            return weatherMain; //return our main list back to the home controller
            

        }
    }
}
