using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace usermanagment.Model
{
    public class project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string projectId { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? description { get; set; }
        [Required]
        public string? technologies { get; set; }
      
    }
}
