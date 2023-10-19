using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tourney_Lad.WebSite.Models
{
	public class Players
	{
        public string Id { get; set; }
        public string Creator { get; set; }
		public string PlayerName { get; set; }
		public string TeamName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string Avatar { get; set; }
    }
}

