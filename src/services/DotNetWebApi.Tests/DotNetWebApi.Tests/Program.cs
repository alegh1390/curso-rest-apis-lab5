using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace DotNetWebApi.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:5000");

            //POST
            Product product = new Product();

            product.title = "Comprar azucar";

            Console.WriteLine("Serializado:" + JsonConvert.SerializeObject(product).ToString());

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(product));
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var responsePost = client.PostAsync("/api/v1/todos", httpContent).GetAwaiter().GetResult();

            if(responsePost.IsSuccessStatusCode)
            {
                Console.WriteLine("Post exitoso");
            }
            else
            {
                Console.WriteLine("Error al realizar el post");
            }

            Console.WriteLine("Response Post:" + responsePost);

            //GET
            var response = client.GetAsync("/api/v1/todos").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Conexion exitosa");

                string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                Console.WriteLine("Content:" + content);

                var jsonArray = JsonConvert.DeserializeObject<JArray>(content);

                Console.WriteLine("Array:" + jsonArray);

                Console.WriteLine("Elemento 0:" + jsonArray[0]);

                Console.WriteLine("Title 0:" + jsonArray[0]["title"]);
            }
            else
            {
                Console.WriteLine("Error en la respuesta");
            }

            Console.WriteLine("Response:" + response);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }

    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool isComplete { get; set; }
    }
}
