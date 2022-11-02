using AutoMapper;
using Sat.Recruitment.Application.Features.Users.Commands.CreateUser;
using Sat.Recruitment.Domain.Users;

namespace Sat.Recruitment.Application.Mapping
{
    public class MappingProfileUser : Profile
    {
        public MappingProfileUser()
        {
            CreateMap<CreateUserCommand, User>();
        }
    }
}
