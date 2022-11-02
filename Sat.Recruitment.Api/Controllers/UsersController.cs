using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Features.Users.Commands.CreateUser;
using Sat.Recruitment.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        //private readonly List<User> _users = new List<User>();
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/create-user")]
        //public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        public async Task<ActionResult<Result>> CreateUser([FromBody] CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        //Validate errors
        /*private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }*/
    }
    
}
