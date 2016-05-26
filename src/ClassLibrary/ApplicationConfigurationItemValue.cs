namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ApplicationConfigurationItemValue
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string ItemKey { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string Value { get; set; }
    }
}
