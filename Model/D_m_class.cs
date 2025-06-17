using System.ComponentModel.DataAnnotations;
using Courier_Service_part_1.Model.Binding_model;

namespace Courier_Service_part_1.Model
{
    public class D_m_class
    {


        [Key] 
       public string d_m_id { get; set; } 
        public string d_m_admin_id {  get; set; }  
        public string delivery_location { get; set; }
        public string d_m_status_id { get; set; }
 
        public string order_id { get; set; }
        public ICollection<OrderClass> order_class { get; set; }
        
     
    }
}
