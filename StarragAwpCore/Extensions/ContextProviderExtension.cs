using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarragAwpCore.Extensions
{
    public static class ContextProviderExtension
    {
        static IHttpContextAccessor httpContextAccessor = null;
        public static IHttpContextAccessor HttpContextAccessor
        {
            get { return httpContextAccessor; }
            set
            {
                if (httpContextAccessor != null)
                {
                    throw new Exception("");
                }
                httpContextAccessor = value;
            }
        }
    }
}
