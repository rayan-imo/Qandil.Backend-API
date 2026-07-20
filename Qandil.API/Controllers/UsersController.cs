
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Children;
using Qandil.API.Dtos.Requests.Users;
using Qandil.API.Dtos.Responses.Children;
using Qandil.API.Dtos.Responses.Users;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Service.Dtos.ChildDto.Request;
using Qandil.Service.Dtos.UserDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<UserResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var users = await _userService.GetAllAsync(paginationParameter);
            return Ok(users?.Value?.MapTo(c => UserResponse.Transform(c)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "المستخدم غير موجود",
                    MessageEn = "User not found"
                });
            }
            var userEntity = result.Value;

            return Ok(new ApiResponse<UserResponse>
            {
                Success = true,
                MessageAr = "تم جلب المستخدم بنجاح",
                MessageEn = "User retrieved successfully",
                Data = UserResponse.Transform(userEntity),
            });
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserRequest userRequest)
        {
            var userDto = new UserRequestdto
            {
                Email = userRequest.Email,
                Password = userRequest.Password,
                Role = userRequest.Role,

            };

            var result = await _userService.AddAsync(userDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<UserResponse>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة المستخدم",
                    MessageEn = "Failed to add user"
                });
            }
            var userEntity = result.Value;

            return Ok(new ApiResponse<UserResponse>
            {
                Success = true,
                MessageAr = "تمت إضافة المستخدم بنجاح",
                MessageEn = "User added successfully",
                Data = UserResponse.Transform(userEntity),
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UserRequest userRequest, Guid id)
        {
            var userDto = new UserRequestdto
            {
                Email = userRequest.Email,
                Password = userRequest.Password,
                Role = userRequest.Role,
            };
            var result = await _userService.UpdateAsync(userDto, id);
            var userEntity = result.Value;
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات المستخدم",
                    MessageEn = "Failed to update user"

                });
            }

            return Ok(new ApiResponse<UserResponse>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات المستخدم بنجاح",
                MessageEn = "User updated successfully",
                Data = UserResponse.Transform(userEntity),
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف المستخدم",
                    MessageEn = "Failed to delete user"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف المستخدم بنجاح",
                MessageEn = "User deleted successfully"
            });
        }
    }
}
