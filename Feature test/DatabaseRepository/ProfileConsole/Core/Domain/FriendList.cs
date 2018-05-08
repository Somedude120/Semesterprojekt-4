namespace ProfileConsole.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FriendList")]
    public partial class FriendList
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string User1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string User2 { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [Required]
        [StringLength(15)]
        public string Action_User { get; set; }

        public virtual UserInformation UserInformation { get; set; }

        public virtual UserInformation UserInformation1 { get; set; }

        public virtual UserInformation UserInformation2 { get; set; }
    }
}
