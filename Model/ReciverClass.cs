using System.ComponentModel.DataAnnotations;

namespace Courier_Service_part_1.Model
{
    public class ReciverClass:PersonClass
    {
        [Key]
        public string reciver_id { get; set; }
        Order_track_Class order_Track { get; set; }
    }
}
