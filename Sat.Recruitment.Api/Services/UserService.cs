using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Model;

namespace Sat.Recruitment.Api.Services
{
    public class UserService: IUserService
    {
        private readonly IUserDao dao;
        private readonly IUserMapper userMapper;

        public UserService(IUserDao dao, IUserMapper userMapper)
        {
            this.dao = dao;
            this.userMapper = userMapper;
        }

        public async Task<ValidationError> AddUser(UserDto user)
        {
            var validationError = new ValidationError();
            var result = await dao.FindBy(x=> x.Email == user.Email || x.Phone == user.Phone || (x.Name == user.Name && x.Address == user.Address));
            if (result.Count() == 0)
            {
                var newUser = userMapper.FromDtoToEntity(user);
                await dao.Add(newUser);
                return validationError;
            }

            validationError.Errors.Add("The user is duplicated");
            return validationError;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var result = await dao.Get(x=>x.Role);
            return result.Select(x => userMapper.FromEntityToDto(x)).ToList();
        }
    }

    public interface IUserService
    {
        Task<ValidationError> AddUser(UserDto user);
        Task<List<UserDto>> GetAll();
    }
}