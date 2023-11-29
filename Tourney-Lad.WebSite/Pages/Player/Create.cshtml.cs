using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Tourney_Lad.WebSite.Areas.Identity.Data;

namespace Tourney_Lad.WebSite.Pages.Player
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public PlayerInfo PlayerInfo = new PlayerInfo();
        public string currentUserID = string.Empty;
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            PlayerInfo.PlayerName = Request.Form["playername"];
            PlayerInfo.TeamName = Request.Form["teamname"];
            PlayerInfo.Wins = Request.Form["wins"];
            PlayerInfo.Losses = Request.Form["losses"];
            PlayerInfo.Avatar = Request.Form["avatar"];

            if (PlayerInfo.PlayerName.Length == 0 || PlayerInfo.TeamName.Length == 0 || PlayerInfo.Wins.Length == 0 || PlayerInfo.Losses.Length == 0)
            {
                errorMessage = "Please enter ALL fields!";
                return;
            }

            try
            {
                var builder = WebApplication.CreateBuilder();
                var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlCmd = "INSERT INTO AuthSystemDB.dbo.Players (Id, PlayerName, TeamName, Wins, Losses, Avatar, Creator) VALUES (@id, @playername, @teamname, @wins, @losses, @avatar, @creator);";
                    using (SqlCommand command = new SqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", Guid.NewGuid().ToString("N"));
                        command.Parameters.AddWithValue("@playername", PlayerInfo.PlayerName);
                        command.Parameters.AddWithValue("@teamname", PlayerInfo.TeamName);
                        command.Parameters.AddWithValue("@wins", PlayerInfo.Wins);
                        command.Parameters.AddWithValue("@losses", PlayerInfo.Losses);
                        command.Parameters.AddWithValue("@avatar", PlayerInfo.Avatar);
                        command.Parameters.AddWithValue("@creator", currentUserID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = PlayerInfo.PlayerName + " has been added!";
            PlayerInfo.PlayerName = "";
            PlayerInfo.TeamName = "";
            PlayerInfo.Wins = "";
            PlayerInfo.Losses = "";
            PlayerInfo.Avatar = "";
        }
    }
}
