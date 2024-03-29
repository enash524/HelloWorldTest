﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HelloWorldTest.WebApi.Tests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            string stringResponse = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        //public static void InitializeDbForTests(NorthwindDbContext context)
        //{
        //    NorthwindInitializer.Initialize(context);
        //}
    }
}
