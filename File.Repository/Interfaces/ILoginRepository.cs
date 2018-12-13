using Common.Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace File.Repository.Interfaces
{
    public interface ILoginRepository
    {
        bool ValidateLoginCredentials(Login login);
    }
}
