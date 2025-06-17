using System.ComponentModel.DataAnnotations;

namespace Courier_Service_part_1.Model
{
    public class Order_track_Class
    {
        [Key]
        public string Order_Id { get; set; }
       public string reciver_id {  get; set; }
        public string sender_id { get; set; }
        public SenderClass sender { get; set; }
        public ReciverClass reciver { get; set; }

    }
}
