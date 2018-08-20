using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using RestSharp;
using RestSharp.Deserializers;
using SimpleJson;

namespace natural.Models
{
    public class Product
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
        public static List<Product> local()
        {
            return new List<Product>() {
                new Product(){first_name = "Uno" },
                new Product(){first_name = "Dos" },
                new Product(){first_name = "Tres" },
                new Product(){first_name = "Cuatro" },
                new Product(){first_name = "Cinco" },
                new Product(){first_name = "Seis" },
                new Product(){first_name = "Siete" },
                new Product(){first_name = "Ocho" },
            };
        }

        public static RestClient get(int id, Func<Product, int> toDo)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://reqres.in/");

            var request = new RestRequest();
            request.Resource = String.Format("api/users/{0}", id);

            IRestResponse response = client.Execute(request);
            ProductObjectRest rootObject = null;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
                rootObject = deserial.Deserialize<ProductObjectRest>(response);
                //return rootObject.data;
            }
            else
            {
                rootObject = new ProductObjectRest();
                //return rootObject.data;
            }

            toDo(rootObject.data);
            return client;
        }
        public static List<Product> sync()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("https://reqres.in/");

            var request = new RestRequest();
            request.Resource = "api/users";

            IRestResponse response = client.Execute(request);
            ProductRest rootObject;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
                rootObject = deserial.Deserialize<ProductRest>(response);
                //return rootObject.data;
            }
            else
            {
                rootObject = new ProductRest();
                //return rootObject.data;
            }
            return rootObject.data;
        }
        public static RestClient sync(Func<List<Product>, int> toDo)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("https://reqres.in/");

            var request = new RestRequest();
            request.Resource = "api/users";

            IRestResponse response = client.Execute(request);
            ProductRest rootObject;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
                rootObject = deserial.Deserialize<ProductRest>(response);
                //return rootObject.data;
            }
            else
            {
                rootObject = new ProductRest();
                //return rootObject.data;
            }
            toDo(rootObject.data);
            return client;
        }
    }
    public class ProductRest
    {
        public List<Product> data { get; set; }
    }
    public class ProductObjectRest
    {
        public Product data { get; set; }
    }
}