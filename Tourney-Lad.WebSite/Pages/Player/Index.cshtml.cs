using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Tourney_Lad.WebSite.Pages.Player
{
    public class IndexModel : PageModel
    {
        public List<PlayerInfo> listPlayers = new List<PlayerInfo>();

        public void OnGet()
        {
            try
            {
                var builder = WebApplication.CreateBuilder();
                var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlCmd = "SELECT * FROM AuthSystemDB.dbo.Players";
                    using (SqlCommand command = new SqlCommand(sqlCmd, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PlayerInfo playerInfo = new PlayerInfo();
                                playerInfo.PlayerName = reader.GetString(1);
                                playerInfo.TeamName = reader.GetString(2);
                                playerInfo.Wins = "" + reader.GetInt32(3);
                                playerInfo.Losses = "" + reader.GetInt32(4);
                                playerInfo.Avatar = reader.GetString(5);

                                listPlayers.Add(playerInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class PlayerInfo
    {
        public string PlayerName;
        public string TeamName;
        public string Wins;
        public string Losses;
        public string Avatar;
    }
}
