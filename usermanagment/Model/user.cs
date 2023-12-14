using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace usermanagment.Model
{
    public class user
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public  string   Role{ get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int  Empid {  get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
