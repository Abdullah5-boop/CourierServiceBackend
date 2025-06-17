using Courier_Service_part_1.Model;
using Courier_Service_part_1.Model.DTO;
using Courier_Service_part_1.Model.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courier_Service_part_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly TokenProviderClass _tokenClass;
        public readonly AdminDbContext _dbcontext;
        public AdminController(AdminDbContext dbcontext, TokenProviderClass tokenClass)
        {
              _dbcontext = dbcontext;
            _tokenClass = tokenClass;

        }
        [HttpGet("D_M")]
        public IActionResult getAll()
        { 
            var d_m_list = _dbcontext.delivery_Man_table.ToList();  
            return Ok(d_m_list);
         }



        [HttpGet("randoms")]
        public string random_fun()
        {
            Random random = new Random();
            int number = random.Next(0, 10000000);
            string str = Convert.ToString(number);

            return str;
        }


        [HttpPost("D_M")]
        public IActionResult add_d_M(D_M_DTO dm)
        {
            string d_m_id = String.Concat("d_M",random_fun());
            var d_m = new D_m_class()
            {
                d_m_id = d_m_id,
                d_m_admin_id = dm.d_m_admin_id,
                delivery_location = dm.delivery_location,
                d_m_status_id = dm.d_m_status_id,
                order_id = dm.order_id,



            };

            _dbcontext.delivery_Man_table.Add(d_m);
            _dbcontext.SaveChanges();

            var status = new Status_SenderCLass()
            {
                IsSuccess = true,
                txt = "done"
            };

            return Ok(status);
        }


        [HttpGet("Login_dm/{id}")]
        public async Task<IActionResult> login_Dm(string id)
        {
            var user = await _dbcontext.delivery_Man_table.FirstOrDefaultAsync(i=>i.d_m_id == id);
            if (user != null) {
                var logindetails = new LoginDTO()
                {
                    email = "DM2gmail.com",
                    password =id,
                    userName ="delivery_man"
                };
                var token = _tokenClass.CreateToken(logindetails);
                var status = new Status_SenderCLass()
                {
                    IsSuccess= true,
                    txt=token

                };
                return Ok(status);
            }
            else
            {
                var status = new Status_SenderCLass()
                {
                    IsSuccess = false,
                    txt = "dm Is not exist"

                };
                return Ok(status);

            }
            
        }

    }
}
