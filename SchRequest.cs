using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using System.Text.Json;

namespace NBA_Schedule_Request
{
    class Program
    {
        static HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
        };
        
        static readonly HttpClient client = new HttpClient(handler);

        static async Task Main()
        {
            await GetSchedule();

            Console.Read();
        }

        static async Task GetSchedule()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://cdn.nba.com/static/json/staticData/scheduleLeagueV2_1.json");
                var responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                
                System.Console.WriteLine("\nException Caught!" );
            }
            
        }

    }

}