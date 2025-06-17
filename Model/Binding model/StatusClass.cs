using System.ComponentModel.DataAnnotations;

namespace Courier_Service_part_1.Model.Binding_model
{
    public class StatusClass
    {
        [Key]
        public string status_Id { get; set; }
        public string Current_status { get; set; }



    }
}
