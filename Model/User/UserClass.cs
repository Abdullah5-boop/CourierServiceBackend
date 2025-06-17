using Microsoft.AspNetCore.Identity;

namespace Courier_Service_part_1.Model.User
{
    public class UserClass:IdentityUser
    {
        public string user_type { get; set; }


    }
}
