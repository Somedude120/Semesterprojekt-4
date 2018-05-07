namespace ProfileConsole
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class ProfileContext : DbContext
    {
        public ProfileContext()
            : base("name=ProfileContext")
        {
        }

        public virtual DbSet<ChatGroups> ChatGroups { get; set; }
        public virtual DbSet<Emoji> Emoji { get; set; }
        public virtual DbSet<FriendList> FriendList { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<UserInformation> UserInformation { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatGroups>()
                .Property(e => e.GroupName)
                .IsUnicode(false);

            modelBuilder.Entity<ChatGroups>()
                .HasMany(e => e.Chat)
                .WithRequired(e => e.ChatGroups)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatGroups>()
                .HasMany(e => e.UserInformation)
                .WithMany(e => e.ChatGroups)
                .Map(m => m.ToTable("UserChatGroups").MapLeftKey("GroupId").MapRightKey("UserName"));

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

            modelBuilder.Entity<Login>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Salt)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Hash)
                .IsUnicode(false);

            modelBuilder.Entity<Tags>()
                .HasMany(e => e.UserInformation)
                .WithMany(e => e.Tags)
                .Map(m => m.ToTable("UserTags").MapLeftKey("TagName").MapRightKey("UserName"));

            modelBuilder.Entity<UserInformation>()
                .Property(e => e.UserName)
                .IsUnicode(false);

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

            modelBuilder.Entity<UserInformation>()
                .HasOptional(e => e.Login)
                .WithRequired(e => e.UserInformation);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Chat)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.Sender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Chat>()
                .Property(e => e.Sender)
                .IsUnicode(false);
        }
    }
}
