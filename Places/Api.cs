using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Places
{
    public static class Api
    {
        private static HttpClient Client { get; set; }
        public static string AppSecret = "AIzaSyCCYGqVWDYDud2l7I9u9Kgi6oxbegFzDTY"; // App Key

        static Api()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/place/");
        }


        /// <summary>
        /// Gets stores of a certain category near the specified co-ordinates. Can be show only
        /// open stores or all stores.
        /// </summary>
        /// <param name="latitude">Latitude of user.</param>
        /// <param name="longitude">Longitude of user.</param>
        /// <param name="category">Category of stores to find.</param>
        /// <param name="open">Should find only open stores.</param>
        async public static Task<Response> GetPlaces(double latitude, double longitude, long radius, Category category)
        {
           string url = null;
                url = String.Format("nearbysearch/json?key={0}&location={1},{2}&radius={3}&types={4}", AppSecret, latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture),radius, category.ToString());
           
            try
            {
                var resp = await Client.GetAsync(url);
                if (resp.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync(), typeof(Response)) as Response;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Gets another set of 20 places that match a previous query.
        /// </summary>
        /// <param name="token">Next Token to fetch.</param>
        async public static Task<Response> GetNext(string token)
        {
            try
            {
                var resp = await Client.GetAsync(String.Format("nearbysearch/json?key={0}&pagetoken={1}", AppSecret, token));
                if (resp.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync(), typeof(Response)) as Response;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Locale search near specified co-ordinates.
        /// </summary>
        /// <param name="latitude">Latitude of user.</param>
        /// <param name="longitude">Longitude of user.</param>
        /// <param name="query">Search query</param>
        async public static Task<Response> SearchPlaces(double latitude, double longitude, string query)
        {
            try
            {
                var resp = await Client.GetAsync(String.Format("nearbysearch/json?key={0}&location={1},{2}&sensor=true&rankby=distance&keyword={3}", AppSecret, latitude, longitude, query));
                if (resp.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync(), typeof(Response)) as Response;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get full details of specified place ID.
        /// </summary>
        /// <param name="placeId">ID of place.</param>
        async public static Task<Detail> GetDetails(string placeId)
        {
            try
            {
                var resp = await Client.GetAsync(String.Format("details/json?key={0}&placeid={1}&sensor=true", AppSecret, placeId));
                if (resp.IsSuccessStatusCode)
                {
                    return (JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync(), typeof(Response)) as Response).Detail;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}