using System;
using System.Dynamic;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.Contracts;
using Sat.Recruitment.Application.Features.Users.Commands.CreateUser;
using Sat.Recruitment.Application.Mapping;
using Sat.Recruitment.Application.Models.Common;
using Sat.Recruitment.Domain.Users;
using Sat.Recruitment.Infrastructure.Users;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {        
        private readonly Mock<IHandleUser> _handleUser;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CreateUserCommandHandler>> _logger;
        private IHandleUser handleService;
        public UnitTest1()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfileUser>();
            });
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<CreateUserCommandHandler>>();
            _handleUser = new Mock<IHandleUser>();
        }

        [Fact]
        public async void Test1()
        {

            var userInput = new CreateUserCommand
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money =  124
            };

            //cheking the handler
            var handler = new CreateUserCommandHandler(_logger.Object, _handleUser.Object, _mapper);
            var result = await handler.Handle(userInput, CancellationToken.None);
            //cheking the response
            var userEntity = _mapper.Map<User>(userInput);
            handleService = new HandleUser();
            var res = handleService.CreateUser(userEntity);


            Assert.True(res.IsSuccess);
            Assert.Equal("User Created", res.Errors);
        }

        [Fact]
        public async void Test2()
        {
            //var userController = new UsersController();

            //var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;
            var userInput = new CreateUserCommand
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };
            //cheking the handler
            var handler = new CreateUserCommandHandler(_logger.Object, _handleUser.Object, _mapper);
            var result = await handler.Handle(userInput, CancellationToken.None);

            //cheking the response
            var userEntity = _mapper.Map<User>(userInput);
            handleService = new HandleUser();
            var res = handleService.CreateUser(userEntity);

            Assert.False(res.IsSuccess);
            Assert.Equal("The user is duplicated", res.Errors);
        }
    }
}
