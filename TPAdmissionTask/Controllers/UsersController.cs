using Microsoft.AspNetCore.Mvc;
using TPAdmissionTask.Models;
using TPAdmissionTask.Interface;
using TPAdmissionTask.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TPAdmissionTask.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers? _IUsers;

        public UsersController(IUsers IUsers)
        {
            _IUsers = IUsers;
        }

        // route => users/
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            if (_IUsers == null)
                return BadRequest("No users found .. ");

            var users = _IUsers.GetUsers();

            if (users == null)
                return BadRequest("No users found ... ");

            return await Task.FromResult(users);
        }

        // route => users/{id}
        [HttpGet("{id}")]
        [Route("users")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var users = await Task.FromResult(_IUsers?.GetUsers(id));
            if (users == null)
                return BadRequest("User not found ... ");

            return users;
        }

        // THIS IS TO GET ALL USERS
        //// route => users/
        [HttpPost]
        [Route("users/create")]
        public async Task<ActionResult<UserModel>> Post(UserModel user)
        {
            var foundUser = _IUsers!.GetUsers()!.ToList().Where(x => x.Email == user.Email);
            if (foundUser.ToList().Count > 0)
                return BadRequest("Email already exists .. ");

            _IUsers?.AddUser(user);
            return await Task.FromResult(user);
        }

        // route => users
        [HttpPost]
        [Route("users")]
        public async Task<ActionResult<UserModel>> Post(LoginRequestModel request)
        {
            var user = _IUsers?.LoginUser(request.Email!, request.Password!);
            if (user == null)
                return BadRequest("Email or password are wrong ... ");
            return await Task.FromResult(user);
        }

        // route => users/{id}
        [HttpPut("{id}")]
        [Route("users")]
        public async Task<ActionResult<UserModel>> Put(int id, UserModel user)
        {
            if (id != user.UserId)
                return BadRequest("Can't find user to update .. ");

            try
            {
                _IUsers?.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return BadRequest("User already exists in database .. ");
                else
                    throw;
            }

            return await Task.FromResult(user);
        }

        // route => users/{id}
        [HttpDelete("{id}")]
        [Route("users")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            var user = _IUsers?.DeleteUser(id);

            if (user == null) return NotFound();

            return await Task.FromResult(user);
        }

        private bool UserExists(int id)
        {
            if (_IUsers == null)
                return false;

            return _IUsers.CheckUser(id);
        }
    }
}
