using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.DataHandlers
{
    public static class ApiHandler
    {
        public static HttpClient ApiClient { get; set; } //making static because we only ever want 1 client per process, dont need multiple instances

        public static void InitializeApi()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://www.metaweather.com/api/location/44544/"); //set to defaul to belfast so we only have to add date to the call
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //only want json in response
        }

    }
}
