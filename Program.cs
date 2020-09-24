using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;

namespace crawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();

            string url = "https://miyaya.github.io";
            var responseMessage = await httpClient.GetAsync(url); // send request

            // check StatusCode==200
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseResult = responseMessage.Content.ReadAsStringAsync().Result; // get response

                //Console.WriteLine(responseResult);

                // configurate AngleSharp
                // AngleSharp: QuerySelector (document.QuerySelector in Javascript)
                var config = Configuration.Default;
                var context = BrowsingContext.New(config);

                // put response result into res.Content()
                var document = await context.OpenAsync(res => res.Content(responseResult));

                // use QuerySelector to find <p>, or any class/id atrribute
                var contents = document.QuerySelectorAll("p");

                foreach (var c in contents)
                {
                    Console.WriteLine(c.TextContent);
                }
            }

            Console.ReadKey();
        }
    }
}
