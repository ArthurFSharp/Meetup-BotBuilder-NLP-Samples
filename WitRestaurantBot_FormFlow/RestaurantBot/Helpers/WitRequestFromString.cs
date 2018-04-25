using Microsoft.Bot.Framework.Builder.Witai.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestaurantBot.Helpers
{
    public static class WitRequestFromString
    {
        public static async Task<WitResult> ToWitResult(string request)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer PMJYAL2YD3MSLRAEJNCCON2LVDUOIELT");
                UriBuilder builder = new UriBuilder("https://api.wit.ai/message");
                builder.Query = $"q={request}";
                var res = await httpClient.GetAsync(builder.Uri);
                var jsonString = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WitResult>(jsonString);
            }
        }
    }
}