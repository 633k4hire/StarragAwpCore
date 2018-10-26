using SqlCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarragAwpCore.Services
{
    public interface ISqlService
    {
        Task<SQL_Request> OpenConnection(string connectionString = null);
        Task<SQL_Request> CloseConnection(SQL_Request request);

        //NEEDS REMOVE
        Task<SQL_Request> AddAsync(SQL_Request request, string AppName, string XmlData, string XmlData2 = "", string XmlData3 = "", string XmlData4 = "", string XmlData5 = "", bool close = true);
        Task<SQL_Request> GetAllAsync(SQL_Request request, bool close = true);
        Task<SQL_Request> GetAsync(SQL_Request request, string AppName, bool close = true);
        Task<SQL_Request> UpdateAsync(SQL_Request request, string AppName, string XmlData, string XmlData2 = "", string XmlData3 = "", string XmlData4 = "", string XmlData5 = "", bool close = true);
        Task<SQL_Request> RemoveAsync(SQL_Request request, string AppName, bool close = true);
    }
}
