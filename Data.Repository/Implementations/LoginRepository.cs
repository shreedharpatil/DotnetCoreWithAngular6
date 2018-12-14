using Data.Repository.DbContext;
using Data.Repository.Interfaces;
using System.Linq;

namespace Data.Repository.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AemContext dbContext;

        public LoginRepository(AemContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool ValidateLoginCredentials(Common.Layer.Models.Login login)
        {
            return this.dbContext.Login.Any(p => p.Username == login.Username && p.Password == login.Password);
        }
    }
}
