namespace ProfileConsole
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BloggingContext : DbContext
    {
        public BloggingContext()
            : base("name=BloggingContext")
        {
        }

        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<Emoji> Emoji { get; set; }
        public virtual DbSet<FriendList> FriendList { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<UserInformation> UserInformation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>()
                .Property(e => e.Users_MessageNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Chat>()
                .Property(e => e.From_User)
                .IsUnicode(false);

            modelBuilder.Entity<Emoji>()
                .Property(e => e.EmojiShortcut)
                .IsUnicode(false);

            modelBuilder.Entity<Emoji>()
                .Property(e => e.Emoji1)
                .IsFixedLength();

            modelBuilder.Entity<FriendList>()
                .Property(e => e.User1)
                .IsUnicode(false);

            modelBuilder.Entity<FriendList>()
                .Property(e => e.User2)
                .IsUnicode(false);

            modelBuilder.Entity<FriendList>()
                .Property(e => e.Status)
                .IsFixedLength();

            modelBuilder.Entity<FriendList>()
                .Property(e => e.Action_User)
                .IsUnicode(false);

            modelBuilder.Entity<Tags>()
                .HasMany(e => e.UserInformation)
                .WithMany(e => e.Tags)
                .Map(m => m.ToTable("UserTags").MapLeftKey("TagName").MapRightKey("UserName"));

            modelBuilder.Entity<UserInformation>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Chat)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.From_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.FriendList)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.Action_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.FriendList1)
                .WithRequired(e => e.UserInformation1)
                .HasForeignKey(e => e.User1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.FriendList2)
                .WithRequired(e => e.UserInformation2)
                .HasForeignKey(e => e.User2)
                .WillCascadeOnDelete(false);
        }
    }
}
