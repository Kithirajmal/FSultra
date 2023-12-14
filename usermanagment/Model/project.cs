using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace usermanagment.Model
{
    public class project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? technologies { get; set; }
      
    }
}
