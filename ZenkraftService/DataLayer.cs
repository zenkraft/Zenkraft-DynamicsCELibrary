using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService
{
    public class DataLayer
    {
        string ApiEndpoint = "https://api.zenkraft.com";
        public enum WebMethod
        {
            GET,
            POST
        }

        public string getResponseFromApi(string apiKey, WebMethod method, string apiFunction)
        {
            return getResponseFromApi(apiKey, method, apiFunction, null);
        }
            public string getResponseFromApi(string apiKey, WebMethod method, string apiFunction, string jsonObj)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("zkkey", apiKey);
            string jsonResponse = string.Empty;

            try
            {
                Uri endPoint = new Uri(ApiEndpoint + apiFunction);
                if (method == WebMethod.GET)
                {
                    var response = client.GetAsync(endPoint).Result;
                    jsonResponse = response.Content.ReadAsStringAsync().Result;
                }
                else if (method == WebMethod.POST)
                {
                    var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(endPoint, content).Result;
                    jsonResponse = response.Content.ReadAsStringAsync().Result;
                }
            }catch(Exception ex)
            {
               
                jsonResponse = ex.Message;
            }


            return jsonResponse;
        }

    }
}
