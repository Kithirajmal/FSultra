using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace usermanagment.Model
{
    public class user
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public  string   role{ get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int  empId {  get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string pwd { get; set; }

    }
}
