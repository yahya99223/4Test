using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EF.Validation.Model
{
    public class Company
    {
        public Company()
        {
            People = new HashSet<Person>();
        }


        [Key]
        public Guid Id { get; set; }


        [Required]
        public string Name { get; set; }


        public ICollection<Person> People { get; set; }
    }
}