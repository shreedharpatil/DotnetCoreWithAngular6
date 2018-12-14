using System;
using System.Collections.Generic;

namespace DotnetCoreWithAngular6.DB
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
