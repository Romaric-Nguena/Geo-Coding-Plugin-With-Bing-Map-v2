using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GeoCoding
{
    class Program
    {
        // Replace this with your Bing Maps API key
        private const string BingMapsApiKey = "AqYzGoLfTyr9d51Bbl5tCexU6UHg4J88320qccuYCFSSLVvuIvs_sr1MuN1OX4Ys";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the address:");
            string address = Console.ReadLine();

            var coordinates = await GetCoordinatesAsync(address);

            if (coordinates != null)
            {
                Console.WriteLine("");
                Console.WriteLine("Geo Coding Results: ");
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine($"Latitude: {coordinates.Value.Latitude}");
                Console.WriteLine($"Longitude: {coordinates.Value.Longitude}");
            }
            else
            {
                Console.WriteLine("Unable to geocode the address.");
            }
        }

        private static async Task<(double Latitude, double Longitude)?> GetCoordinatesAsync(string address)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $"http://dev.virtualearth.net/REST/v1/Locations?q={Uri.EscapeDataString(address)}&key={BingMapsApiKey}";
                HttpResponseMessage response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseBody);
                    var resourceSets = json["resourceSets"] as JArray;

                    if (resourceSets != null && resourceSets.Count > 0)
                    {
                        var resources = resourceSets[0]["resources"] as JArray;

                        if (resources != null && resources.Count > 0)
                        {
                            var point = resources[0]["point"]["coordinates"] as JArray;

                            if (point != null)
                            {
                                double latitude = (double)point[0];
                                double longitude = (double)point[1];
                                return (latitude, longitude);
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
