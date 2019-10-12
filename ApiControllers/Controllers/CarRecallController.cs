using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiControllers.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    public class CarRecallController : Controller 
    {
        private IRepository repository;

        public IConfiguration Configuration { get; set; }

        public CarRecallController(IRepository repo, IConfiguration config)
        {
            repository = repo;
            Configuration = config;
        }

        [HttpGet("years")]
        public IEnumerable<Year> GetYears() => repository.years;

        [HttpGet("makes")]
        public IEnumerable<Make> GetMakes() => repository.makes;

        [HttpGet("{make}/{year}")]
        public async Task<IEnumerable<Model>> GetModelsByMakeYear(string make, string year)
        {
            //need to get a list of models
            using (var client = new HttpClient())
            {
                try
                {
                    dynamic download1 = ProcessURLAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformakeyear/make/" + make + "/modelyear/" + year + "/vehicleType/car?format=json", client);
                    dynamic list1 = await download1;

                    var modelList = new List<Model>();
                    JObject modelsData = JObject.Parse(list1.Value);
                    JArray modelsArray = (JArray)modelsData["Results"];
                    for (int i = 0; i < modelsArray.Count; i++)
                    {
                        var model = new Model();
                        JObject jData = JObject.Parse(modelsArray[i].ToString());
                        model.model = jData["Model_Name"].ToString();
                        modelList.Add(model);
                    }
                    return modelList;
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
        }

        public async Task<dynamic> ProcessURLAsync(string URL, HttpClient client)
        {
            HttpResponseMessage response;
            var content = "";
            
            using (client)
            {
                try
                {
                    response = await client.GetAsync(URL);
                    content = await response.Content.ReadAsStringAsync();

                    var result = await client.GetAsync(URL);
                    result.EnsureSuccessStatusCode();
                    dynamic json = await result.Content.ReadAsStringAsync();
                    return Ok(json);
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
        }

        //return a JSON object with the following:
        //car model
        //car recall information from NHTSA
        //image URL from Bing
        [HttpGet("{make}/{year}/{model}")]
        public async Task<CarRecall> GetCarRecall(string make, string year, string model)
        {
            HttpResponseMessage response;
            string content = "";
            var strResp = "";
            var recalls = "";
            var wrap = new Wrapper();
            var carRecall = new CarRecall();
            //create a new 
            var myModel = new CarInfoModel
            {
                make = make,
                year = year,
                model = model
            };
            myModel.model = model;
            carRecall.model = myModel;
            carRecall.recallList = new List<CarRecallItem>();

            //get the recalls info
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://one.nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + year + "/make/"
                        + make + "/model/" + model + "?format=json");
                    response.EnsureSuccessStatusCode();
                    content = await response.Content.ReadAsStringAsync();
                    
                    JObject dataResult = JObject.Parse(content);
                    var recallSection = dataResult.GetValue("Results");
                    
                    if (recallSection.HasValues)
                    {
                        var recallItems = recallSection.ToObject<List<CarRecallItem>>();
                        foreach (var item in recallItems)
                        {
                            carRecall.recallList.Add(item);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }

            if(carRecall.recallList.Count() != 0)
            {
                //if no content is returned, don't get the image
                //get the API key from secrets file
                var client2 = new HttpClient();
                var accountKey = Configuration["Bing:ServiceAPIKey"];
                // Request headers  
                client2.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", accountKey);
                // Request parameters
                string query = year + " " + make + " " + model;
                string count = "1";
                string offset = "0";
                var ImgSearchEndPoint = "https://api.cognitive.microsoft.com/bing/v7.0/images/search?";
                var result = await client2.GetAsync(string.Format("{0}q={1}&count={2}&offset={3}", ImgSearchEndPoint, WebUtility.UrlEncode(query), count, offset));
                result.EnsureSuccessStatusCode();
                var json = await result.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse(json);
                var items = data.value;
                carRecall.imageUrl = items[0].contentUrl;
            }
            else
            {
                carRecall.imageUrl = "No request made";
            }

            return carRecall;
        }
    }
}
