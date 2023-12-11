using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Bson.IO;
using Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly MongoDBUserService _mongoDBService;
        private readonly AuthService _authService;
        private readonly AuthenticateResponse _authenticateResponse;

        public UsersController(MongoDBUserService mongoDBService,AuthService authService, AuthenticateResponse authenticateResponse)
        {
            _mongoDBService = mongoDBService;
            _authService = authService;
            _authenticateResponse = authenticateResponse;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest(new { message = "Please enter email and password!" });
            }

            List<Users> existingUserList = await _mongoDBService.GetAsync(login.Email);
            if (existingUserList == null || existingUserList.Count==0)
            {
                return BadRequest(new { message = "Please enter the correct email id" });
            }
            if (existingUserList[0].Password == login.Password) {
                string token = _authService.generateJwtToken(existingUserList[0]);
                var response= new AuthenticateResponse(existingUserList[0], token);
                return Ok(response);
            }

            return BadRequest(new { message = "Please enter the correct password" });
        }
        [HttpPost]
        [Route("registration")]
        public async Task<JsonResult> AddUserAsync([FromBody] Users user)
        {
            if (user != null)
            {
                //check email is already present in the database or not

                List<Users> existingUserList = await _mongoDBService.GetAsync(user.Email);
                if (existingUserList.Count() > 0 && existingUserList != null)
                {
                    return Json("This email id already exists in the system");
                }
                await _mongoDBService.CreateAsync(user);
                return Json("Registration is successfull");

            }
            return Json("Please input the required details");
        }
        [Authorize]
        [HttpGet]
        public async Task<List<Users>> GetUsersAsync()
        {
            List<Users> users = await _mongoDBService.GetAllAsync();
            return users;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users user)
        {
            await _mongoDBService.CreateAsync(user);
            return CreatedAtAction(nameof(GetUsersAsync), new { id = user.Id }, user);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> AddToUsers(string id, [FromBody] string name)
        {
            await _mongoDBService.AddToUserAsync(id, name);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}
