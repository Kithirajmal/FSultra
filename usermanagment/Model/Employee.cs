﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace usermanagment.Model
{
    public class Employee
    {
        public Employee()
        {
            resourceallocations = new HashSet<Resourceallocation>();
        }
        [Key]
        public int Id { get; set; }
        [Required]

        public string empId { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public DateTime dob { get; set; }
        [Required]
        public DateTime doj { get; set; }


        public string? pwd { get; set; }

        public bool isAllocated { get; set; }
        public string? currentProject { get; set; }
        public string? primarySkillSet { get; set; }
        public string? secondarySkillSet { get; set; }

        public ICollection<Resourceallocation> resourceallocations { get; set; }
        //public virtual Resourceallocation resourceallocation { get; set; }


    }
}
