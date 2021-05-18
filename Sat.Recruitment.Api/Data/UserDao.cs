using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Model;

namespace Sat.Recruitment.Api.Data
{
    public class UserDao : WritableEntityRepository<User, string>, IUserDao
    {
        public UserDao(UserContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IUserDao : IWritableEntityRepository<User, string>
    {
    }

    public class SuperUserDao : WritableEntityRepository<SuperUser, string>, ISuperUserDao
    {
        public SuperUserDao(UserContext dbContext) : base(dbContext)
        {
        }
    }

    public interface ISuperUserDao : IWritableEntityRepository<SuperUser, string>
    {
    }
}