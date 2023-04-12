using EntityFrameworkDemo.Model;
using Microsoft.Extensions.Configuration;
// using Microsoft.AspNet.WebApi.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace HttpClientConsole
{
    internal class Program
    {
        private static HttpClient _httpClient;

        private static string _XClientId;
        private static string _passKey;

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            _XClientId = config["EMTMadrid:X-ClientId"];
            _passKey = config["EMTMadrid:passKey"];

            EchoPostman();
            EchoPostmanSpecialized();

            // _httpClient = new HttpClient();
            // _httpClient.BaseAddress = new Uri("http://localhost:5243/api/");

            // GetCustomers1();
            // GetCustomers2();
            // GetCustomers3();
            // GetCustomers4();

            // PostCustomer();
            // PutCustomer();
            // DeleteCustomer();

            // _httpClient.BaseAddress = new Uri("https://api.zippopotam.us/");

            // GetPostCode();
            // GetPostCodeDynamic();
            // GetPostCodeDynamicNative();

            // _httpClient.BaseAddress = new Uri("https://openapi.emtmadrid.es/");
        }

        static void EchoPostman()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("https://postman-echo.com/");

            // Default/shared headers
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("User-Agent", "MyHttpClientConsole (v1.0.0)");
            http.DefaultRequestHeaders.Add("X-Param", "qwerty");

            http.DefaultRequestHeaders.Add("Accept", "application/json");
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var obj = new { Name = "Manuel", Country = "Spain" };
            var body = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = http.PostAsync("post?p1=demo1&p2=demo2", body).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(data);
            }
        }

        static void EchoPostmanSpecialized()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("https://postman-echo.com/");

            // Default/shared headers
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("User-Agent", "MyHttpClientConsole (v1.0.0)");
            http.DefaultRequestHeaders.Add("X-Param", "qwerty");

            // Specialized headers
            var request = new HttpRequestMessage(HttpMethod.Post, "post?p1=demo1&p2=demo2");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var obj = new { Name = "Manuel", Country = "Spain" };
            request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = http.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(data);
            }
        }

        static void GetCustomers1()
        {
            // Console.Clear();
            Console.Write("Insert Customer ID: ");

            var customerId = Console.ReadLine();

            HttpResponseMessage response = _httpClient.GetAsync($"customers/{customerId}").Result;

            if (response.IsSuccessStatusCode) { }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                Customer customer = JsonConvert.DeserializeObject<Customer>(data);

                Console.WriteLine($"Customer: {customer.CompanyName} - {customer.Country}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        static void GetCustomers2()
        {
            // Console.Clear();
            Console.Write("Insert Customer ID: ");

            var customerId = Console.ReadLine();

            HttpResponseMessage response = _httpClient.GetAsync($"customers/{customerId}").Result;

            var customer = response.StatusCode == HttpStatusCode.OK ?
                JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result) :
                null;

            if (customer != null)
            {
                Console.WriteLine($"Customer: {customer.CompanyName} - {customer.Country}");
            }
        }

        static void GetCustomers3()
        {
            // Console.Clear();
            Console.Write("Insert Customer ID: ");

            var customerId = Console.ReadLine();

            HttpResponseMessage response = _httpClient.GetAsync($"customers/{customerId}").Result;

            var customer = response.StatusCode == HttpStatusCode.OK ?
                JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result) :
                null;

            if (customer != null)
            {
                Console.WriteLine($"Customer: {customer.companyName} - {customer.country}");
            }
        }

        static void GetCustomers4()
        {
            // Console.Clear();
            Console.Write("Insert Customer ID: ");

            var customerId = Console.ReadLine();

            Customer customer = _httpClient.GetFromJsonAsync<Customer>($"customers/{customerId}").Result;

            if (customer != null)
            {
                Console.WriteLine($"Customer: {customer.CompanyName} - {customer.Country}");
            }
            else
            {
                Console.WriteLine("Customer not found");
            }
        }

        static void PostCustomer()
        {
            Console.Clear();
            Console.Write("Insert customer ID: ");

            var customerId = Console.ReadLine();

            Customer customer = new Customer()
            {
                CustomerID = customerId,
                CompanyName = "Un Dos Tres Responda Otra Vez, SAU",
                ContactName = $"Manuel",
                City = "Madrid",
                Country = "Spain"
            };

            string jsonCustomer = JsonConvert.SerializeObject(customer);

            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync("customers", content).Result;

            if (response.StatusCode == HttpStatusCode.Created)
            {
                Customer responseCustomer = JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine($"Customer created with ID: {responseCustomer.CustomerID}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        static void PutCustomer()
        {
            Console.Clear();
            Console.Write("Insert customer ID: ");

            var customerId = Console.ReadLine();

            var customer = _httpClient.GetFromJsonAsync<Customer>($"customers/{customerId}").Result;

            customer.ContactName = "Manolo";
            customer.Phone = "+34910911912";

            string jsonCustomer = JsonConvert.SerializeObject(customer);

            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync($"customers/{customerId}", content).Result;

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                Console.WriteLine($"Customer modified with ID: {customerId}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        static void DeleteCustomer()
        {
            Console.Clear();
            Console.Write("Insert customer ID: ");

            var customerId = Console.ReadLine();

            HttpResponseMessage response = _httpClient.DeleteAsync($"customers/{customerId}").Result;

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                Console.WriteLine($"Customer with ID {customerId} deleted correctly");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        static void GetPostCode()
        {
            Console.Clear();
            
            Console.Write("Insert country code: ");
            var countryCode = Console.ReadLine();

            Console.Write("Insert post code: ");
            var postCode = Console.ReadLine();

            Console.WriteLine(string.Empty);

            var countryPostCode = _httpClient.GetFromJsonAsync<CountryPostCode>($"{countryCode}/{postCode}").Result;

            if (countryPostCode != null)
            {
                Console.WriteLine($"Post code: {countryPostCode.PostCode} | Country: {countryPostCode.Country}");

                foreach ( var place in countryPostCode.Places )
                {
                    Console.WriteLine($"Place: {place.PlaceName}, {place.State} ({place.StateAbbreviation})");
                }
            }
        }

        static void GetPostCodeDynamic()
        {
            Console.Clear();

            Console.Write("Insert country code: ");
            var countryCode = Console.ReadLine();

            Console.Write("Insert post code: ");
            var postCode = Console.ReadLine();

            Console.WriteLine(string.Empty);

            // Throws exception because native JSON parser is crap LOL
            // Now, seriously, seems to be unable to parse to a dynamic object
            // (idk if because of keys with spaces or directly for being dynamic)
            //
            // var countryPostCode = _httpClient.GetFromJsonAsync<dynamic>($"{countryCode}/{postCode}").Result;

            HttpResponseMessage response = _httpClient.GetAsync($"{countryCode}/{postCode}").Result;

            var countryPostCode = response.StatusCode == HttpStatusCode.OK ?
                JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result) :
                null;

            if (countryPostCode != null)
            {
                Console.WriteLine($"Post code: {countryPostCode["post code"]} | Country: {countryPostCode.country}");

                foreach (var place in countryPostCode.places)
                {
                    Console.WriteLine($"Place: {place["place name"]}, {place.state} ({place["state abbreviation"]})");
                }
            }
        }

        static void GetPostCodeDynamicNative()
        {
            Console.Clear();

            Console.Write("Insert country code: ");
            var countryCode = Console.ReadLine();

            Console.Write("Insert post code: ");
            var postCode = Console.ReadLine();

            Console.WriteLine(string.Empty);

            var countryPostCode = _httpClient.GetFromJsonAsync<dynamic>($"{countryCode}/{postCode}").Result;

            Console.WriteLine($"Post code: {countryPostCode.GetProperty("post code")} | Country: {countryPostCode.GetProperty("country")}");

            foreach ( var place in countryPostCode.GetProperty("places").EnumerateArray() )
            {
                Console.WriteLine($"Place: {place.GetProperty("place name")}, {place.GetProperty("state")} ({place.GetProperty("state abbreviation")})");
            }
        }

        static void GetArrivalsEMT()
        {
            Console.Clear();
            Console.Write("Insert stop ID: ");

            var stopId = Console.ReadLine();

            Console.WriteLine(string.Empty);

            // And so on...
        }

        static void LoginEMT()
        {

        }

        static void LogoutEMT()
        {

        }
    }

    public class CountryPostCode
    {
        // https://stackoverflow.com/a/72685036
        // https://stackoverflow.com/a/14187931
        [JsonPropertyName("post code")]
        public string PostCode { get; set; }
        
        [JsonPropertyName("country")]
        public string Country { get; set; }
        
        [JsonPropertyName("places")]
        public List<Place> Places { get; set; }
        // public List<Dictionary<string, string>> Places { get; set; }
    }

    public class Place
    {
        [JsonPropertyName("place name")]
        public string PlaceName { get; set; }
        
        [JsonPropertyName("state")]
        public string State { get; set; }
        
        [JsonPropertyName("state abbreviation")]
        public string StateAbbreviation { get; set; }
        
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }
}