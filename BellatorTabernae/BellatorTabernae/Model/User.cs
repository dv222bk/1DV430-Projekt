﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BellatorTabernae.Model
{
    [Serializable]
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Användaren måste ha ett namn!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Användarens namn får inte vara längre än 50 tecken!")]
        public string Username { get; set; }
    }
}