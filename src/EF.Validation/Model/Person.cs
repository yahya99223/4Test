using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EF.Validation.Model
{
    public class Person
    {
        public Person()
        {
            Offices = new HashSet<Office>();
        }


        [Key]
        public Guid Id { get; set; }


        [Index("UQ_Person_Name", IsUnique = true, Order = 1)]
        [Index("UQ_Person_Email", IsUnique = true, Order = 1)]
        public Guid CompanyId { get; set; }

        public Guid? ManagerId { get; set; }


        [StringLength(30)]
        [Index("IX_Person_Name")]
        [Index("UQ_Person_Name", IsUnique = true, Order = 3)]
        public string Name { get; set; }


        [StringLength(30)]
        [Index("IX_Person_Email")]
        [Index("UQ_Person_Email", IsUnique = true, Order = 3)]
        public string Email { get; set; }


        public Company Company { get; set; }
        public Person Manager { get; set; }
        public ICollection<Office> Offices { get; set; }
    }
}