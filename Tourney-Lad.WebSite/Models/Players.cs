using Microsoft.AspNetCore.Mvc;
using System;

namespace Tourney_Lad.WebSite.Models
{
	public class Players
	{
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        public string PlayerName { get; set; }
        [BindProperty]
        public string TeamName { get; set; }
        [BindProperty]
        public int Wins { get; set; }
        [BindProperty]
        public int Losses { get; set; }
        [BindProperty]
        public string Avatar { get; set; }
        [BindProperty]
        public string Creator { get; set; }
    }
}

