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
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MessageNumber { get; set; }

        public string Message { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string Sender { get; set; }

        public virtual ChatGroups ChatGroups { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
