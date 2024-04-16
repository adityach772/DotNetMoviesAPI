using System;
using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public partial class Registration
    {
        public string Uname { get; set; } = null!;
        public string? Email { get; set; }
        public string? Passwrd { get; set; }
    }
}
