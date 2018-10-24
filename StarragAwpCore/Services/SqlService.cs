using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlCore;

namespace StarragAwpCore.Services
{
    public class SqlService : ISqlService
    {
        public SQL_Request _request = new SQL_Request();

        public async Task<SQL_Request> AddAsync(SQL_Request request, string AppName, string XmlData, string XmlData2 = "", string XmlData3 = "", string XmlData4 = "", string XmlData5 = "", bool close = true)
        {
            return await request.SettingsAddAsync(AppName, XmlData, XmlData2, XmlData3, XmlData4, XmlData5, close);
        }

        public async Task<SQL_Request> CloseConnection(SQL_Request request)
        {
            return await request.CloseConnection();
        }

        public async Task<SQL_Request> GetAllAsync(SQL_Request request, bool close = true)
        {
            return await request.SettingsGetAllAsync(close);
        }

        public async Task<SQL_Request> GetAsync(SQL_Request request, string AppName, bool close = true)
        {
            return await request.SettingsGetAsync(AppName,close);
        }

        public async Task<SQL_Request> OpenConnection(string connectionString = null)
        {
            if (connectionString==null)
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

        public async Task<SQL_Request> UpdateAsync(SQL_Request request, string AppName, string XmlData, string XmlData2 = "", string XmlData3 = "", string XmlData4 = "", string XmlData5 = "", bool close = true)
        {
            return await request.SettingsUpdateAsync(AppName, XmlData, XmlData2, XmlData3, XmlData4, XmlData5, close);
        }
    }
}
