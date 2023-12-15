using System.ComponentModel.DataAnnotations.Schema;

namespace usermanagment.Model
{
    public class Resourceallocation
    {
        public int id { get; set; }
        public int employeeid { get; set; }

        public int projectid { get; set; }

        public string projectName { get; set; }

        public  bool iscurrent {  get; set; }
           
        public string startdate { get; set; }

        public string enddate { get; set;}
        [NotMapped]
        public virtual Employee employee { get; set; }


    }
}
