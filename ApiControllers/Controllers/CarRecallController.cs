using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiControllers.Models;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Dynamic;

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    public class CarRecallController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        private IRepository repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public IConfiguration Configuration { get; set; }

        public CarRecallController(IRepository repo, IConfiguration config, IHttpClientFactory clientFac)
        {
            repository = repo;
            Configuration = config;
            clientFactory = clientFac;
        }

        [HttpGet("years")]
        public IEnumerable<Year> GetYears() => repository.years;

        [HttpGet("makes")]
        public IEnumerable<Make> GetMakes() => repository.makes;

        [HttpGet("{make}/{year}")]
        public async Task<IEnumerable<Model>> GetModelsByMakeYear(string make, string year)
        {
            //need to get a list of models
            using (var client = clientFactory.CreateClient())
            {
                try
                {
                    dynamic download1 = ProcessURLAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformakeyear/make/" + make + "/modelyear/" + year + "/vehicleType/car?format=json", client);
                    dynamic list1 = await download1;

                    var modelList = new List<Model>();
                    var modelsData = JsonSerializer.Deserialize<MakeSearchResult>(list1.Value);
                    var modelsArray = modelsData.Results;
                    for (int i = 0; i < modelsArray.Count; i++)
                    {
                        var model = new Model();
                        model.model = modelsArray[i].Model_Name;
                        modelList.Add(model);
                    }
                    return modelList;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public async Task<dynamic> ProcessURLAsync(string URL, HttpClient client)
        {
            using (client)
            {
                try
                {
                    var result = await client.GetAsync(URL);
                    result.EnsureSuccessStatusCode();
                    dynamic json = await result.Content.ReadAsStringAsync();
                    return Ok(json);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
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
            var wrap = new Wrapper();
            var carRecall = new CarRecall();
            //create a new model
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
            using (var client = clientFactory.CreateClient())
            {
                client.BaseAddress = new Uri("https://one.nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + year + "/make/"
                        + make + "/model/" + model + "?format=json");
                    response.EnsureSuccessStatusCode();
                    content = await response.Content.ReadAsStringAsync();

                    //JObject dataResult = JObject.Parse(content);
                    var dataResult = JsonSerializer.Deserialize<RecallObject>(content);
                    var recallSection = dataResult.Results;
                    
                    if (recallSection.Count > 0)
                    {
                        foreach (var item in recallSection)
                        {
                            var recallItem = new CarRecallItem { 
                                Manufacturer = item.Manufacturer,
                                NHTSACampaignNumber = item.NHTSACampaignNumber,
                                ReportReceivedDate = item.ReportReceivedDate.ToString(),
                                Component = item.Component,
                                Summary = item.Summary,
                                Conequence = item.Conequence,
                                Remedy = item.Remedy,
                                Notes = item.Notes,
                                ModelYear = item.ModelYear,
                                Make = item.Make,
                                Model = item.Model
                            };
                            carRecall.recallList.Add(recallItem);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            if(carRecall.recallList.Count() != 0)
            {
                //if no content is returned, don't get the image
                //get the API key from secrets file
                var client2 = clientFactory.CreateClient();
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
                var resObject = JsonDocument.Parse(json);
                var rootObj = resObject.RootElement;
                var items = rootObj.GetProperty("value");
                carRecall.imageUrl = items[0].GetProperty("contentUrl").ToString();
            }
            else
            {
                carRecall.imageUrl = "No request made";
            }

            return carRecall;
        }
    }
}
