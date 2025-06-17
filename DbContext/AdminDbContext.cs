using Courier_Service_part_1.Model;
using Microsoft.EntityFrameworkCore;



public class AdminDbContext : DbContext
{
    public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
    {}
    public DbSet<D_m_class> delivery_Man_table { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {

        modelBuilder.Entity<D_m_class>()
            .HasMany(o => o.order_class)
            .WithOne(o => o.D_M_Class).HasForeignKey(o => o.delivery_man_id);

    }
}

