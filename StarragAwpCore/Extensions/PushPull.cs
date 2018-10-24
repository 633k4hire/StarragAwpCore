using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarragAwpCore
{
    public static class Push
    {
        public static async Task UserData()
        {
            await Task.CompletedTask;
        }
    }

    public static class Pull
    {
        public static async Task<string> UserData()
        {
            return await Task.Run<string>(()=> { return "test"; });
        }
    }
}
