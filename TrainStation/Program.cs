using System;
using Places;
namespace TrainStation
{
    class Program
    {
        static void Main(string[] args)
        {
           
            
            retreiveTrainStation();

            var x = r;
           

            Console.WriteLine("Hello World!");
        }

        static Response r;

        public static async void retreiveTrainStation()
        {
            Category c = Category.train_station;
            r =   Api.GetPlaces(48.871697, 2.319479,50000 ,c).Result;
            distanceBetweenAll(r, 48.871697, 2.319479);
            while (r.Next != null)
            {
                r =  Api.GetNext(r.Next).Result;
                distanceBetweenAll(r, 48.871697, 2.319479);
            }
        }

        public static void distanceBetweenAll(Response results, double lat, double longt)
        {
            foreach (var point in results.Places)
            {
                var distance = Helper.CalculateDistance(lat, longt, point.Geo.Location.Latitude, point.Geo.Location.Longitude);
                Console.WriteLine(string.Format("La distance entre le point original et {0} est de {1}m", point.Name, distance));

            }
        }

    }


   
}
