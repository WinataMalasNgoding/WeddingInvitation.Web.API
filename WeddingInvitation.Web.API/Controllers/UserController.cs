using Microsoft.AspNetCore.Mvc;
using WeddingInvitation.Web.API.Contract;
using WeddingInvitation.Web.API.Models;

namespace WeddingInvitation.Web.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHelperRepository _helperRepository;

        public UserController(IUserRepository userRepository, IHelperRepository helperRepository)
        {
            _userRepository = userRepository;
            _helperRepository = helperRepository;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(long userId)
        {
            try
            {
                var result = _userRepository.GetUserById(userId).Result;

                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetUserAll()
        {
            try
            {
                var result =  _userRepository.GetUserGridAll().Result;

                if (result.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult InsertUser([FromBody] UserInsertModel userInput)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_helperRepository.ValidateIsFieldExists("User", "Username", userInput.Username).Result)
                {
                    return Conflict("Username already exists.");
                }
                var result = _userRepository.InsertUser(userInput);

                return CreatedAtAction(nameof(InsertUser), result.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(long userId, [FromBody] UserUpdateModel userInput)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_helperRepository.ValidateIsFieldExists("User", "UserId", userId).Result)
                {
                    return NotFound();
                }

                var result = _userRepository.UpdateUser(userId, userInput);

                return StatusCode(200, new { Message = $"User (Username : {(result.Result is not null ? result.Result.Username : "-")}) have been updated!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(long userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_helperRepository.ValidateIsFieldExists("User", "UserId", userId).Result)
                {
                    return NotFound();
                }

                var result = _userRepository.DeleteUser(userId);

                return StatusCode(200, new { Message = result.Result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
