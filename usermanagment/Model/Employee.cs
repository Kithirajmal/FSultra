using System.ComponentModel.DataAnnotations;

namespace usermanagment.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string email { get; set; }
        public string primaryskill { get; set; }
        public string secondaryskill { get; set; }
     }
}
