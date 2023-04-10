using AutoMapper;
using ECommerce_App.DTOs;
using ECommerce_App.Errors;
using ECommerce_App.Extensions;
using ECommerce_App.Models;
using ECommerce_App.Models.Identity;
using ECommerce_App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce_App.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet]


        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {




            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName

            };

        }

        [HttpGet("emailexists")]

        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)

        {

            return await _userManager.FindByEmailAsync(email) != null;


        }



        [Authorize]
        [HttpGet("address")]



        public async Task<ActionResult<AddressDto>> GetUserAddress()

        {



            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

            return _mapper.Map<Address, AddressDto>(user.Address);

        }

        [Authorize]
        [HttpPut("address")]

        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user =  await _userManager.FindUserByClaimsPrincipleWithAddress(HttpContext.User); //Gives us the user with the Address

            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);      //updates the users email address

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));//return the new updated Address.

            return BadRequest("Problem updating the user"); //If error return a bad request.
        }


        [HttpPost("Login")]


        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) 
        
        { 
        
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));//If user doesn't exist return Unauthorized

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password , false);

            if (!result.Succeeded)  return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName

            };
         
        }

        [HttpPost("register")]


        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        
        {

            var user = new AppUser
            {
                DisplayName= registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email


            };

            var result = await _userManager.CreateAsync(user, registerDto.Password); //Creates the User

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,

            };
        
        }


    
    }
    
}
