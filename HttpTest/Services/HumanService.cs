using HttpTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpTest.Services
{
    public class Rootobject
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public Human[] data { get; set; }
    }

    public class Human
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }
    public class HumanService
    {
        public string URL { get; set; } = "https://reqres.in/api/users";
        public async Task<List<Human>?> GetAllAsync()
        {
            HttpClient client = new HttpClient();
            var result=await client.GetAsync(URL+ "?page=1");
            var jsonString=await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Rootobject>(jsonString);
            return data?.data.ToList();
        }

        public async Task<string> DeleteById(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.DeleteAsync(URL + "/"+id.ToString());
            var responseMessage = result.StatusCode;
            return responseMessage.ToString();
        }

        public async Task<HumanCreateModel> AddHumanAsync(HumanCreateModel model)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(URL, data);

            var result = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<HumanCreateModel>(result);
            return obj;
        }

        public async Task<HumanUpdateModel> UpdateHumanAsync(int id,HumanUpdateModel model)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(URL+"/"+id, data);

            var result = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<HumanUpdateModel>(result);
            return obj;
        }
    }
}
