using Microsoft.AspNetCore.Mvc;
using api.Interfaces;

namespace api.Controllers{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController{
        private readonly IUserRepository _repository;
    }
}