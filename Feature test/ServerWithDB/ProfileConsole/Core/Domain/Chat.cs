namespace ProfileConsole.Core.Domain
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
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Sender { get; set; }

        [Required]
        [StringLength(15)]
        public string Receiver { get; set; }

        public string Message { get; set; }

        public virtual UserInformation UserInformation { get; set; }

        public virtual UserInformation UserInformation1 { get; set; }
    }
}
