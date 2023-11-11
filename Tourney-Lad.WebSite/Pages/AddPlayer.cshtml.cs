using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Tourney_Lad.WebSite.Areas.Identity.Data;
using Tourney_Lad.WebSite.Models;
using System.Diagnostics;


namespace Tourney_Lad.WebSite.Pages
{
    [Authorize]
    public class AddPlayerModel : PageModel
    {
        public void OnGet()
        {
            
        }

        [HttpPost]
        public IActionResult PlayerSubmit(Players player, UserManager<ApplicationUser> UserManager)
        {
            Debug.WriteLine("lmao");

            var builder = WebApplication.CreateBuilder();
            var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'DB' not found.");

            var sql = "INSERT INTO dbo.Players (Id, PlayerName, TeamName, Wins, Losses, Avatar, Creator) VALUES (@Id, @PlayerName, @TeamName, @Wins, @Losses, @Avatar, @Creator)";
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString("N"));
            command.Parameters.AddWithValue("@PlayerName", player.PlayerName);
            command.Parameters.AddWithValue("@TeamName", player.TeamName);
            command.Parameters.AddWithValue("@Wins", player.Wins);
            command.Parameters.AddWithValue("@Losses", player.Losses);
            command.Parameters.AddWithValue("@Avatar", player.Avatar);
            command.Parameters.AddWithValue("@Creator", UserManager.GetUserAsync(User).Result?.Id);
            connection.Open();
            command.ExecuteNonQuery();

            return RedirectToAction("Index");
        }
    }
}
