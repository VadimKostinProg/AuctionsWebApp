using BidMasterOnline.Application.Constants;
using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidMasterOnline.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("list")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<ListModel<UserDTO>>> GetUsersList([FromQuery] UserSpecificationsDTO specifications)
        {
            return Ok(await _userManager.GetUsersListAsync(specifications));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> GetUserByUsername([FromRoute] Guid id)
        {
            return Ok(await _userManager.GetUserByIdAsync(id));
        }

        [HttpPost("customers")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateCustomer([FromForm] CreateUserDTO user)
        {
            await _userManager.CreateUserAsync(user, Application.Enums.UserRole.Customer);

            return Ok("Account has been successfully created.");
        }

        [HttpPost("technical-support-specialists")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<string>> CreateTechnicalSupportSpecialist([FromForm] CreateUserDTO user)
        {
            await _userManager.CreateUserAsync(user, Application.Enums.UserRole.TechnicalSupportSpecialist);

            return Ok("Account has been successfully created.");
        }

        [HttpPost("admins")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<string>> CreateAdmin([FromForm] CreateUserDTO user)
        {
            await _userManager.CreateUserAsync(user, Application.Enums.UserRole.Admin);

            return Ok("Account has been successfully created.");
        }

        [HttpPut("passwords")]
        [Authorize]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            await _userManager.ChangePasswordAsync(changePassword);

            return Ok("Password has been successfully changed.");
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<string>> DeleteOwnAccount()
        {
            await _userManager.DeleteUserAsync();

            return Ok("Your account has been deleted successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<string>> DeleteAccount([FromRoute] Guid id)
        {
            await _userManager.DeleteUserAsync(id);

            return Ok("User account has been deleted successfully.");
        }

        [HttpPut("{id}/block")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.TechnicalSupportSpecialist}")]
        public async Task<ActionResult<string>> BlockUser([FromRoute] Guid id, [FromQuery] int? days)
        {
            await _userManager.BlockUserAsync(id, days);

            return Ok("User has been blocked successfully.");
        }

        [HttpPut("{id}/unblock")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.TechnicalSupportSpecialist}")]
        public async Task<ActionResult<string>> UnblockUser([FromRoute] Guid id)
        {
            await _userManager.UnblockUserAsync(id);

            return Ok("User has been unblocked successfully.");
        }
    }
}
