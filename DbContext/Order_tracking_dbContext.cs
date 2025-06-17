using Courier_Service_part_1.Model;
using Courier_Service_part_1.Model.Binding_model;
using Microsoft.EntityFrameworkCore;



public class Order_tracking_dbContext : DbContext
{
    public Order_tracking_dbContext(DbContextOptions<Order_tracking_dbContext> options) : base(options)
    { }
    
    public DbSet<Order_track_Class> order_Track {  get; set; }
    public DbSet<SenderClass> sender_table { get; set; }
    public DbSet<ReciverClass> reciver_table { get;set; }
    public DbSet<OrderClass> order_table { get; set; }

 


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order_track_Class>()
            .HasOne(o => o.reciver)
            .WithOne().HasForeignKey<Order_track_Class>(o => o.reciver_id);

        modelBuilder.Entity<Order_track_Class>()
            .HasOne(o=>o.sender)
            .WithOne().HasForeignKey<Order_track_Class>(o => o.sender_id);

        modelBuilder.Entity<OrderClass>()
            .HasOne(o => o.order_Track)
            .WithOne().HasForeignKey<OrderClass>(o => o.order_track_id);



        modelBuilder.Entity<D_m_class>()
            .HasMany(o=>o.order_class)
            .WithOne(o=>o.D_M_Class).HasForeignKey(o=>o.delivery_man_id);
   
    }






}

