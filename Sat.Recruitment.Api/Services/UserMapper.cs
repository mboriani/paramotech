using System;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Model;

namespace Sat.Recruitment.Api.Services
{
    public class UserMapper : IUserMapper
    {
        public User FromDtoToEntity(UserDto dto)
        {
            var user = new User()
            {
                Address = dto.Address,
                Name = dto.Name,
                Phone = dto.Phone,
            };
            user.Email = this.NormalizeEmail(dto.Email);
            switch (dto.UserType)
            {
                case "Premium":
                    user.Role = new Premium();
                    break;
                case "SuperUser":
                    user.Role = new SuperUser();
                    break;
                default: user.Role = new Normal();
                    break;
            }

            user.Money = dto.Money;

            return user;
        }

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        public UserDto FromEntityToDto(User user)
        {
            return new UserDto()
            {
                Name = user.Name,
                UserType = user.Role.GetType().Name,
                Phone = user.Phone,
                Email = user.Email,
                Address = user.Address,
                Money = user.Money
            };
        }
    }

    public interface IUserMapper
    {
        User FromDtoToEntity(UserDto dto);
        UserDto FromEntityToDto(User user);
    }
}