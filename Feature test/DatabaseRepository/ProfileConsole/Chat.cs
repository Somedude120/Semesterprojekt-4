namespace ProfileConsole
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chat")]
    public partial class Chat
    {
        [Key]
        [StringLength(255)]
        public string Users_MessageNumber { get; set; }

        public string Message { get; set; }

        [Required]
        [StringLength(15)]
        public string From_User { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
