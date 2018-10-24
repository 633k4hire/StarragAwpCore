using SqlCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarragAwpCore
{
    public static class SqlServiceExtensions
    {
        public static async Task<SQL_Request> AddAsync(this SQL_Request request, string AppName, string XmlData, string XmlData2 = "", string XmlData3 = "", string XmlData4 = "", string XmlData5 = "", bool close = true)
        {
            return await request.SettingsAddAsync(AppName, XmlData, XmlData2, XmlData3, XmlData4, XmlData5, close);
        }

        public static async Task<SQL_Request> CloseConnection(this SQL_Request request)
        {
            return await request.CloseConnection();
        }

        public static async Task<SQL_Request> GetAllAsync(this SQL_Request request, bool close = true)
        {
            return await request.SettingsGetAllAsync(close);
        }

        public static async Task<SQL_Request> GetAsync(this SQL_Request request, string AppName, bool close = true)
        {
            return await request.SettingsGetAsync(AppName, close);
        }

        public static async Task<SQL_Request> OpenConnection(string connectionString = null)
        {
            if (connectionString == null)
            {
                SQL_Request request = new SQL_Request();
                return await Task.FromResult<SQL_Request>(request.OpenConnection(SQLfunc._ConnectionString));
            }
            else
            {
                SQL_Request request = new SQL_Request();
                return await Task.FromResult<SQL_Request>(request.OpenConnection(connectionString));
            }

        }

        public static async Task<SQL_Request> UpdateAsync(this SQL_Request request, string AppName, string XmlData, string XmlData2 = "", string XmlData3 = "", string XmlData4 = "", string XmlData5 = "", bool close = true)
        {
            return await request.SettingsUpdateAsync(AppName, XmlData, XmlData2, XmlData3, XmlData4, XmlData5, close);
        }
    }
}
