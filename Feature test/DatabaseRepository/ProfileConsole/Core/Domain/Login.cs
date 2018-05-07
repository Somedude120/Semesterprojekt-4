namespace ProfileConsole
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Login")]
    public partial class Login
    {
        [Key]
        [StringLength(15)]
        public string UserName { get; set; }

        public string Salt { get; set; }

        public string Hash { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
