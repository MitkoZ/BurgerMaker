using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByNameAndPassword(string username, string password);
        void RegisterUser(User user, string password);
    }
}
