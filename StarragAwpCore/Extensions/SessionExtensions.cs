using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarragAwpCore.Extensions
{
    public static class SessionExtensions
    {
        public static void Push<T>(this ISession session, string key, T value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }

        public static T Pull<T>(this ISession session, string key)
        {
            session.TryGetValue(key, out byte[] dataByte);
            string data = dataByte != null ? Encoding.UTF8.GetString(dataByte) : null;

            return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }


        public static async Task PushAsync<T>(this ISession session, string key, T value)
        {
            await Task.Run(()=> { session.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value))); });
        }

        public static async Task<T> PullAsync<T>(this ISession session, string key)
        {
           return  await Task.FromResult<T>(Pull<T>(session,key));
        }
    }
}
