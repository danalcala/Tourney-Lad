using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Tourney_Lad.WebSite.Models;

namespace Tourney_Lad.WebSite.Pages
{
    [Authorize]
    public class AddPlayerModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
