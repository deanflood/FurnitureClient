using FurnitureService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureWeather
{
    class Program
    {
        const string BASE_URL = "http://localhost:59159/";

        static async Task Run()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URL);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    List<Furniture> furnitureList = new List<Furniture>();
                    HttpResponseMessage response = await client.GetAsync("api/furniture/");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        furnitureList = await response.Content.ReadAsAsync<List<Furniture>>();
                        foreach (Furniture f in furnitureList)
                        {
                            Console.WriteLine(f);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task RunType()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URL);                             // base URL for API Controller i.e. RESTFul service

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Console.WriteLine("Enter type (CHAIR, BED, TABLE");
                    string furnitureType = Console.ReadLine();

                    List<Furniture> furnitureList = new List<Furniture>();
                    HttpResponseMessage response = await client.GetAsync("api/furniture/type/" + furnitureType);
                 
                    if (response.IsSuccessStatusCode)
                    {
                        furnitureList = await response.Content.ReadAsAsync<List<Furniture>>();
                        foreach (Furniture f in furnitureList)
                        {
                            Console.WriteLine(f);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task RunPost()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URL);                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Console.WriteLine("Enter type (CHAIR, BED, TABLE)");
                    FurnitureService.Models.Type furnitureType = (FurnitureService.Models.Type) Enum.Parse(typeof(FurnitureService.Models.Type), Console.ReadLine());
                    Console.WriteLine("Enter name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Price");
                    double price = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Avail (TRUE, FALSE)");
                    bool avail = bool.Parse(Console.ReadLine());

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/furniture/add", new Furniture(name, furnitureType, price, avail));
                    if (response.IsSuccessStatusCode)
                    {     
                            Console.WriteLine("POSTED");
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task RunPut()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URL);                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Console.WriteLine("Enter ID");
                    int id = int.Parse(Console.ReadLine());
                   
                    Console.WriteLine("Enter Avail (TRUE, FALSE)");
                    bool avail = bool.Parse(Console.ReadLine());

                    Furniture update = new Furniture
                    {
                        Id = id,
                        IsAvailable = avail
                    };

                    HttpResponseMessage response = await client.PutAsJsonAsync("api/furniture/update", update);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("POSTED");
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task RunDelete()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URL);                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Console.WriteLine("Enter ID");
                    int id = int.Parse(Console.ReadLine());

                    HttpResponseMessage response = await client.DeleteAsync("api/furniture/delete/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("DELETED");
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main()
        {
            int selection = 0;


            while (selection != -1)
            {
                Console.WriteLine("==== Make Selection ====");
                Console.WriteLine("1. View All ");
                Console.WriteLine("2. View by type");
                Console.WriteLine("3. Add Furniture");
                Console.WriteLine("4. Update Furniture");
                Console.WriteLine("5. Delete Furniture");
                Console.WriteLine("-1. Exit");
                Console.WriteLine("=======================");

                selection = int.Parse(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        Run().Wait();
                        break;
                    case 2:
                        RunType().Wait();
                        break;
                    case 3:
                        RunPost().Wait();
                        break;
                    case 4:
                        RunPut().Wait();
                        break;
                    case 5:
                        RunDelete().Wait();
                        break;
                }
            }
        }
    }
}
