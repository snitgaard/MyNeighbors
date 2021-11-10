using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MyNeighbors.Core.Entity;
using Newtonsoft.Json;

namespace MyNeighbors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //GetAddresses();
            CreateHostBuilder(args).Build().Run();
        }

        static JsonSerializerSettings GetJsonSettings()
        {
            JsonSerializerSettings objjsonSettings = new JsonSerializerSettings();
            objjsonSettings.Error = (serializer, err) => err.ErrorContext.Handled = true;
            objjsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            objjsonSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            return objjsonSettings;
        }

        async static void GetAddresses()
        {
            try
            {
                var url = "https://api.dataforsyningen.dk/adresser?stedid=12337669-a544-6b98-e053-d480220a5a3f&struktur=mini&vejnavn=Skjoldsgade";
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                var res = await client.SendAsync(request);
                var responseString = await res.Content.ReadAsStringAsync();

                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var addressprop = JsonConvert.DeserializeObject<List<Address>>(responseString, GetJsonSettings());
                    foreach (Address address in addressprop)
                    {
                        System.Diagnostics.Debug.WriteLine(address.Description);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
