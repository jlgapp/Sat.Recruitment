using Sat.Recruitment.Application.Models.Common;
using Sat.Recruitment.Domain.Users;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Contracts
{
    public interface IHandleUser
    {
        public Result CreateUser(User newUser);
        StreamReader ReadUsersFromFile();
    }
}
