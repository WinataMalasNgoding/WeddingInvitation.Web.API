using Microsoft.AspNetCore.Mvc;
using WeddingInvitation.Web.API.Contract;
using WeddingInvitation.Web.API.Models;
using WeddingInvitation.Web.API.Repositories;

namespace WeddingInvitation.Web.API.Controllers
{
    [ApiController]
    [Route("api/theme")]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeRepository _themeRepository;
        private readonly IHelperRepository _helperRepository;
        
        public ThemeController(IThemeRepository themeRepository, IHelperRepository helperRepository)
        {
            _themeRepository = themeRepository;
            _helperRepository = helperRepository;
        }

        [HttpGet("{themeId}")]
        public IActionResult GetTheme(long themeId)
        {
            try
            {
                var result = _themeRepository.GetThemeById(themeId).Result;

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
        public IActionResult GetThemeAll()
        {
            try
            {
                var result = _themeRepository.GetThemeGridAll().Result;

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
        public IActionResult InsertTheme([FromBody] ThemeInsertModel themeInput)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_helperRepository.ValidateIsFieldExists("Theme", "ThemeName", themeInput.ThemeName).Result)
                {
                    return Conflict("Theme name already exists.");
                }
                var result = _themeRepository.InsertTheme(themeInput);

                return CreatedAtAction(nameof(InsertTheme), result.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{themeId}")]
        public IActionResult UpdateTheme(long themeId, [FromBody] ThemeUpdateModel themeInput)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_helperRepository.ValidateIsFieldExists("Theme", "ThemeId", themeId).Result)
                {
                    return NotFound();
                }

                var result = _themeRepository.UpdateTheme(themeId, themeInput);

                return StatusCode(200, new { Message = $"Theme (Name : {(result.Result is not null ? result.Result.ThemeName : "-")}) have been updated!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{themeId}")]
        public IActionResult DeleteTheme(long themeId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_helperRepository.ValidateIsFieldExists("Theme", "ThemeId", themeId).Result)
                {
                    return NotFound();
                }

                var result = _themeRepository.DeleteTheme(themeId);

                return StatusCode(200, new { Message = result.Result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
