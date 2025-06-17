using Courier_Service_part_1.Model.DTO;
using Courier_Service_part_1.Model.Service;
using Courier_Service_part_1.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courier_Service_part_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndentityController : ControllerBase
    {
        public readonly UserManager<UserClass> _ums;
        public readonly SignInManager<UserClass> _sm;
        public readonly TokenProviderClass _tokenClass;
        public IndentityController(UserManager<UserClass> um, SignInManager<UserClass> sm, TokenProviderClass tokenClass)
        {
            _ums = um;
            _sm = sm;
            _tokenClass = tokenClass;

        }




        [HttpPost("reg")]
        public async Task<IActionResult> AddUser(UserInfoInputDTO user) 
        { 
            if(user == null) { return BadRequest("input is not asign"); }

            var User = new UserClass
               {
                UserName = user.UserEmail,
                user_type = user.user_type,
                Email = user.UserEmail,
                PasswordHash = user.Password,


            };
            bool exist = await _ums.Users.AnyAsync(u => u.Email == user.UserEmail);
            if (exist) {
                var status = new Status_SenderCLass
                {
                    IsSuccess = false,
                    txt = "Email is already exist"


                };
                return BadRequest(status);

            }

            var create_user = await _ums.CreateAsync(User,user.Password);
     

            if (create_user.Succeeded)
            {
                var status = new Status_SenderCLass
                {
                    IsSuccess = true,
                    txt = "user created"


                };

                return Ok(status);
            }
            else
            {
               
                var status = new Status_SenderCLass
                {
                    IsSuccess = false,
                    txt = create_user.Errors.First().Description

                };
                return BadRequest(status);
            }
            

        }
        [HttpGet("find-username")]
        public async Task<IActionResult> Find_userName(string username)
        {
            if (username != null) {
            bool exist =  await _ums.Users.AnyAsync(u => u.UserName == username);
                if (exist)
                {
                    return Ok(false);
                }
                else { return Ok(true); }

            }
            return BadRequest();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login_User(LoginDTO user)
        {
            if (user == null) { return BadRequest("input not assign"); }
            var User = await _ums.FindByEmailAsync(user.email);
            if (User !=null) {
                
                var result = await _sm.CheckPasswordSignInAsync(User,user.password,false);
                if (result.Succeeded) 
                {
                    var token = _tokenClass.CreateToken(user);
                    var login_result = new TokenSendDTO()
                    {
                        Token = token,
                        IsConfirmed = true
                    };

                    return Ok(login_result);
                }
                else
                {
                    var results = await _sm.CheckPasswordSignInAsync(User, user.password, false);
                    return BadRequest("credential error = "+ results);
                }
            }
            else
            { return BadRequest("account is not created"); }

            
        }


        [Authorize]
        [HttpPost("getuser")]
        
        public async Task<IActionResult>  NEW_API(UserDetailsDTO user) {
            if(user==null)
            { return BadRequest("email is not assign"); }
            var ppl = await _ums.Users.FirstOrDefaultAsync(u => u.Email == user.UserEmail);
            if (ppl == null) { return Unauthorized("user is not valid"); }
            return Ok(ppl);
        }

  



    }
}
