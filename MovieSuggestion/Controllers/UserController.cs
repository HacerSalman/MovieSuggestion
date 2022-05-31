using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSuggestion.Core.DTO;
using MovieSuggestion.Core.Services;
using MovieSuggestion.Core.Utils;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UP = MovieSuggestion.Data.Enums.Permission.Values;

namespace MovieSuggestion.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserCreateDTO userDTO)
        {
            var UserToCreate = _mapper.Map<UserCreateDTO, User>(userDTO);
            var newUser = await _userService.CreateUser(UserToCreate);
            await _userService.CreateUserPermission(newUser.Id);
            return _mapper.Map<User, UserDTO>(newUser);
        }

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUserById(ulong id)
        {
            var user = await _userService.GetUserById(id);
            return _mapper.Map<User, UserDTO>(user);
        }


        [HttpGet]
        [MovieAuthorize(UP.USER_MANAGE)]
        public async Task<ActionResult<List<UserDTO>>> GetUserList()
        {
            var userList = await _userService.GetAllUsers();
            return _mapper.Map<IEnumerable<User>, List<UserDTO>>(userList);
        }

        [HttpPut]
        [MovieAuthorize(UP.USER_MANAGE)]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UserUpdateDTO userDTO)
        {
            var user = _mapper.Map<UserUpdateDTO, User>(userDTO);
            var newUser = await _userService.UpdateUser(user);
            return _mapper.Map<User, UserDTO>(newUser);
        }

        [HttpDelete]
        [MovieAuthorize(UP.USER_MANAGE)]
        public async Task<ActionResult<UserDTO>> DeleteUser(ulong userId)
        {
            var user = await _userService.GetUserById(userId);
            return _mapper.Map<User, UserDTO>(await _userService.DeleteUser(user));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            return await _userService.Login(loginDTO.Email, loginDTO.Password);
        }
    }
}
