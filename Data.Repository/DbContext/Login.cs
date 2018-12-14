using System;
using System.Collections.Generic;

namespace Data.Repository.DbContext
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
