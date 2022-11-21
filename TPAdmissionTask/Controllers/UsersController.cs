using Microsoft.AspNetCore.Mvc;
using TPAdmissionTask.Models;
using TPAdmissionTask.Interface;
using TPAdmissionTask.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TPAdmissionTask.Controllers
{
    [Route("users")]
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
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            if (_IUsers == null)
                return NotFound();

            var users = _IUsers.GetUsers();

            if (users == null)
                return NotFound();

            return await Task.FromResult(users);
        }

        // route => users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var users = await Task.FromResult(_IUsers?.GetUsers(id));
            if (users == null)
                return NotFound();

            return users;
        }

        // route => users/
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel user)
        {
            _IUsers?.AddUser(user);
            return await Task.FromResult(user);
        }

        // route => users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Put(int id, UserModel user)
        {
            if (id != user.UserId)
                return BadRequest();

            try
            {
                _IUsers?.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                else
                    throw;
            }

            return await Task.FromResult(user);
        }

        // route => users/{id}
        [HttpDelete("{id}")]
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
