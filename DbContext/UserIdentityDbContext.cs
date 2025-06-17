using Courier_Service_part_1.Model.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Courier_Service_part_1.DbContext
{
    public class UserIdentityDbContext : IdentityDbContext<UserClass>
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options)
        { }
    }
}
