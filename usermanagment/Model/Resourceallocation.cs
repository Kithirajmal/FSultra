namespace usermanagment.Model
{
    public class Resourceallocation
    {
        public int id { get; set; }
        public int empid { get; set; }
        public int projectid { get; set; }

        public  bool iscurrent {  get; set; }
           
        public DateTime startdate { get; set; }

        public DateTime enddate { get; set;}
            
     
    }
}
