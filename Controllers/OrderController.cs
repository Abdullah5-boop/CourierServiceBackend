using System.Data.SqlClient;
using Courier_Service_part_1.Model;
using Courier_Service_part_1.Model.DTO;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courier_Service_part_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Order_tracking_dbContext _dbcontext;
       private readonly IConfiguration _configuration;
        public OrderController(Order_tracking_dbContext dbcontext,IConfiguration config)
        {
            _configuration = config;
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult Getorder() {
            var order_code = _dbcontext.order_Track.ToList();
            //Random num = new Random();
            //int number = num.Next(1000, 1000000000);
            //string code = Convert.ToString(number);
            //return Ok(String.Concat("r1",number));
            return Ok(order_code);
        }


        [HttpGet("sender")]
        public IActionResult GetSender()
        {
            var sender = _dbcontext.sender_table.ToList();

            return Ok(sender);
        }


        [HttpGet("reciver")]
        public IActionResult GetReciver()
        {
            var recivers = _dbcontext.reciver_table.ToList();

            return Ok(recivers);
        }

        [HttpGet("random")]
        public string random_fun()
        {
            Random random = new Random();
            int number = random.Next(0, 10000000);
            string str = Convert.ToString(number);

            return str;
        }

        [HttpPost]
        public IActionResult PostSender( Order_traking_DTO req)
        {
           var sd = req.sd;
            var rd = req.rd;
            var odr = req.odr;



            string sender_code = String.Concat("s1", random_fun());




            string reciver_code = String.Concat("r1", random_fun());

            var send_val = new SenderClass()
            {
                Name = sd.Name,
                Location = sd.Location,
                zip_code  = sd.zip_code,
                personType =  sd.personType,
                unique_email=sd.unique_email,
                unique_phone =sd.unique_phone,
                sender_id = sender_code,

            };
            var recive_val = new ReciverClass()
            {
                Name = rd.Name,
                Location = rd.Location,
                zip_code = rd.zip_code,
                personType = rd.personType,
                unique_email = rd.unique_email,
                unique_phone = rd.unique_phone,
                reciver_id = reciver_code,

            };


            string order_code = String.Concat("order", random_fun());



            var Order_tracking = new Order_track_Class()
            {
                Order_Id = order_code,
                reciver_id = reciver_code,
                sender_id = sender_code,
            };
            DateTime date = DateTime.Now;
            string today_date = date.ToString("yyyy-MM-dd HH:mm:ss");
            var order_place = new OrderClass()
            {
                order_track_id = order_code,
                product_name = odr.product_name,
                product_description = odr.product_description,
                delivery_man_id = odr.delivery_man_id,

                product_type = odr.product_type,
                order_date = today_date,
                order_price = odr.order_price,
                order_statuss = odr.order_statuss,
                order_wigth = odr.order_wigth,

            };
            try { 


            _dbcontext.sender_table.Add(send_val);
            _dbcontext.reciver_table.Add(recive_val);
            _dbcontext.order_Track.Add(Order_tracking);
            _dbcontext.order_table.Add(order_place);
            _dbcontext.SaveChanges();
                var status = new Status_SenderCLass
                {
                    IsSuccess = true,
                    txt = order_code

                };
                return Ok(status);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
           

            
        }

        [HttpGet("all_order")]
        public IActionResult GetUsers()
        {
            var users =  _dbcontext.order_table.ToList();
            return Ok(users);
        }

        [HttpPut]
        public IActionResult assign_order_into_dm(Staatus_update value)
        {

            var exist =
                _dbcontext.order_table.FirstOrDefault(i => i.order_track_id ==  value.target_id );
            if(exist!=null)
            {
                exist.delivery_man_id = value.updated_value;
                _dbcontext.SaveChanges();
                var status = new Status_SenderCLass()
                {
                    IsSuccess = true,
                    txt = "successful"
                };
                return Ok(status);
            }
            else
            {
                var status = new Status_SenderCLass()
                {
                    IsSuccess = false,
                    txt = "user does not exist"
                };
                return Ok(status);
            }
            return Ok(value);
        }
        [HttpGet("dapper_class")]
        public async Task<IActionResult> getOrderDetails()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
            var data = await connection.QueryAsync<Demo_dapper>("select order_id, product_name,order_track_id, delivery_man_id, sender_id,list1.reciver_id,order_date , Location from (select ot.order_Id as order_id, product_name ,otc.Order_Id as order_track_id ,ot.delivery_man_id , sender_id, reciver_id,order_date from order_table ot join order_Track otc on ot.order_track_id = otc.Order_Id) as list1 join reciver_table rt on list1.reciver_id = rt.reciver_id order by Location");
            return Ok(data);
        }

        [HttpGet("dm_dashbord/{id}")]
        public async Task<IActionResult> dm_table(string id)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
            var data = await connection.QueryAsync<Dm_dashbordDTO>
                ($"select * from delivery_Man_table dtbl right join (select order_track_id,  product_name , delivery_man_id ,Name, unique_phone , unique_email,order_statuss from order_table ot join (select Name, Order_Id,otc.reciver_id , otc.sender_id, zip_code, unique_email,unique_phone from order_Track otc join reciver_table rc on rc.reciver_id = otc.reciver_id) list on ot.order_track_id = list.Order_Id where delivery_man_id ='{id}') list2 on dtbl.d_m_id = list2.delivery_man_id ");
            if (data != null)
            {


                return Ok(data);
            }
            else
            {
                var status = new Status_SenderCLass()
                {
                    IsSuccess = false,
                    txt = "user does not exist"
                };
                return BadRequest(status);
            }

          
        }
        [HttpPut("order_status_update")]
        public async Task<IActionResult> dm_status_update(Staatus_update status)
        {
            var exist = await _dbcontext.order_table.FirstOrDefaultAsync(i => i.order_track_id == status.target_id);
            if (exist != null)
            {
                exist.order_statuss = status.updated_value;
                _dbcontext.SaveChanges();

                var result = new Status_SenderCLass()
                {
                    IsSuccess = true,
                    txt = "updated"
                };
                return BadRequest(result);
            }
            else {
                var result = new Status_SenderCLass()
                {
                    IsSuccess = false,
                    txt = "user does not exist"
                };
                return BadRequest(result);

            }

            
        }
    }

}
