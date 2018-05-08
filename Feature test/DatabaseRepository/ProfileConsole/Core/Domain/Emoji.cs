namespace ProfileConsole.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Emoji")]
    public partial class Emoji
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string EmojiShortcut { get; set; }

        [Key]
        [Column("Emoji", Order = 1)]
        [StringLength(10)]
        public string Emoji1 { get; set; }
    }
}
