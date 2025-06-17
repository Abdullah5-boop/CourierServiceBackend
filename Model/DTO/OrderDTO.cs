namespace Courier_Service_part_1.Model.DTO
{
    public class OrderDTO
    {
        //public int order_Id { get; set; }
       
        public string product_name { get; set; }
        public string product_description { get; set; }
        public string delivery_man_id { get; set; }
        public string product_type { get; set; }
        //public string order_date { get; set; }
        public double order_price { get; set; }
        public string order_statuss { get; set; }
        public double order_wigth { get; set; }
    
    }
}
