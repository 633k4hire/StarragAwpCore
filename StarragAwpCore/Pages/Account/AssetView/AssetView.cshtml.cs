using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarragAwpCore.Pages.Account.AssetView
{
    public class AssetViewModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "This is the AssetView Page";
        }
    }
}