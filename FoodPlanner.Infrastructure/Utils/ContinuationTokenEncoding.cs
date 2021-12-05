using System;
using System.Text;
using Newtonsoft.Json;

namespace FoodPlanner.Infrastructure.Utils
{
    public class ContinuationTokenEncoding : IContinuationTokenEncoding
    {
        public ContinuationToken Decode(string token)
        {
            var jsonBytes = Convert.FromBase64String(token);
            var json = Encoding.UTF8.GetString(jsonBytes);
            return JsonConvert.DeserializeObject<ContinuationToken>(json);
        }

        public string Encode(ContinuationToken token)
        {
            var json = JsonConvert.SerializeObject(token);
            var jsonByte = Encoding.UTF8.GetBytes(json);
            return Convert.ToBase64String(jsonByte);
        }
    }
}