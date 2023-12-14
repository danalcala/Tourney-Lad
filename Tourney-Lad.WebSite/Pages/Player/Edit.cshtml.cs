using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Tourney_Lad.WebSite.Pages.Player
{
    public class EditModel : PageModel
    {
        public PlayerInfo PlayerInfo = new PlayerInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                var builder = WebApplication.CreateBuilder();
                var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlCmd = "SELECT * FROM AuthSystemDB.dbo.Players WHERE Id=@id";
                    using (SqlCommand command = new SqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                PlayerInfo.Id = reader.GetString(0);
                                PlayerInfo.PlayerName = reader.GetString(1);
                                PlayerInfo.TeamName = reader.GetString(2);
                                PlayerInfo.Wins = "" + reader.GetInt32(3);
                                PlayerInfo.Losses = "" + reader.GetInt32(4);
                                PlayerInfo.Avatar = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            PlayerInfo.Id = Request.Form["id"];
            PlayerInfo.PlayerName = Request.Form["playername"];
            PlayerInfo.TeamName = Request.Form["teamname"];
            PlayerInfo.Wins = Request.Form["wins"];
            PlayerInfo.Losses = Request.Form["losses"];
            PlayerInfo.Avatar = Request.Form["avatar"];

            if( PlayerInfo.PlayerName.Length == 0 || PlayerInfo.TeamName.Length == 0 || PlayerInfo.Wins.Length == 0 || PlayerInfo.Losses.Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            try
            {
                var builder = WebApplication.CreateBuilder();
                var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlCmd = "UPDATE AuthSystemDB.dbo.Players SET PlayerName=@playername, TeamName=@teamname, Wins=@wins, Losses=@losses, Avatar=@avatar WHERE Id=@id";
                    using (SqlCommand command = new SqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@playername", PlayerInfo.PlayerName);
                        command.Parameters.AddWithValue("@teamname", PlayerInfo.TeamName);
                        command.Parameters.AddWithValue("@wins", PlayerInfo.Wins);
                        command.Parameters.AddWithValue("@losses", PlayerInfo.Losses);
                        command.Parameters.AddWithValue("@avatar", PlayerInfo.Avatar);
                        command.Parameters.AddWithValue("@id", PlayerInfo.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Player/Index");
        }
    }
}
