using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Contracts;
using Sat.Recruitment.Application.Models.Common;
using Sat.Recruitment.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IHandleUser _handleUser;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IHandleUser handleUser, IMapper mapper)
        {
            _logger = logger;
            _handleUser = handleUser;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<User>(request);
            var result = _handleUser.CreateUser(userEntity);

            return result;
        }
    }
}
