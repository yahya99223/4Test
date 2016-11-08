using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EF.Validation.Model
{
    public class Office
    {
        [Key]
        [Column(Order = 1)]
        public Guid CountryId { get; set; }


        [Key]
        [Column(Order = 2)]
        public Guid CityId { get; set; }


        public Guid? PersonId { get; set; }


        [Index("UQ_Address_Name", IsUnique = true)]
        [StringLength(50)]
        public string Name { get; set; }
    }
}