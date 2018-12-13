using Common.Layer.Models;
using File.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace File.Repository.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        public bool ValidateLoginCredentials(Login login)
        {
            var data = System.IO.File.ReadAllText("../File.Repository/Tables/Login.json");
            var logins = JsonConvert.DeserializeObject<IEnumerable<Login>>(data);
            return logins.Any(p => p.Username == login.Username && p.Password == login.Password);
        }
    }
}
