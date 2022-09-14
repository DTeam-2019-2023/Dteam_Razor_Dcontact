//using System;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace Dcontact.Data
//{
//    public partial class Web_ProjectContext : IdentityDbContext<IdentityUser>
//    {

//        public Web_ProjectContext(DbContextOptions<Web_ProjectContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<TbAccount> TbAccounts { get; set; } = null!;
//        public virtual DbSet<TbAdmin> TbAdmins { get; set; } = null!;
//        public virtual DbSet<TbAvatar> TbAvatars { get; set; } = null!;
//        public virtual DbSet<TbBackGround> TbBackGrounds { get; set; } = null!;
//        public virtual DbSet<TbDcontact> TbDcontacts { get; set; } = null!;
//        public virtual DbSet<TbLogin> TbLogins { get; set; } = null!;
//        public virtual DbSet<TbOrderInformation> TbOrderInformations { get; set; } = null!;
//        public virtual DbSet<TbReport> TbReports { get; set; } = null!;
//        public virtual DbSet<TbRowContent> TbRowContents { get; set; } = null!;
//        public virtual DbSet<TbRowDesign> TbRowDesigns { get; set; } = null!;
//        public virtual DbSet<TbTemplate> TbTemplates { get; set; } = null!;
//        public virtual DbSet<TbUser> TbUsers { get; set; } = null!;
//        public virtual DbSet<TbVerifyCode> TbVerifyCodes { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = Web_Project; User ID = sa; Password=123456 ; integrated security = True; Encrypt=False");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            modelBuilder.Entity<TbAccount>(entity =>
//            {
//                entity.ToTable("tbAccount");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

//                entity.Property(e => e.UserName)
//                    .IsUnicode(false)
//                    .HasColumnName("userName");
//            });

//            modelBuilder.Entity<TbAdmin>(entity =>
//            {
//                entity.ToTable("tbAdmin");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.Address)
//                    .IsUnicode(true)
//                    .HasColumnName("address");

//                entity.Property(e => e.FullName).HasColumnName("fullName");

//                entity.Property(e => e.Gmail)
//                    .IsUnicode(false)
//                    .HasColumnName("gmail");

//                entity.Property(e => e.PhoneNumber)
//                    .IsUnicode(false)
//                    .HasColumnName("phoneNumber");

//                entity.HasOne(d => d.IdNavigation)
//                    .WithOne(p => p.TbAdmin)
//                    .HasForeignKey<TbAdmin>(d => d.Id)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbAdmin_tbAccount");
//            });

//            modelBuilder.Entity<TbAvatar>(entity =>
//            {
//                entity.ToTable("tbAvatar");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.PictureLocation)
//                    .IsUnicode(false)
//                    .HasColumnName("pictureLocation");
//            });

//            modelBuilder.Entity<TbBackGround>(entity =>
//            {
//                entity.ToTable("tbBackGround");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.PictureLocation)
//                    .IsUnicode(false)
//                    .HasColumnName("pictureLocation");
//            });

//            modelBuilder.Entity<TbDcontact>(entity =>
//            {
//                entity.ToTable("tbDcontact");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.IdUser)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_User");

//                entity.HasOne(d => d.IdUserNavigation)
//                    .WithMany(p => p.TbDcontacts)
//                    .HasForeignKey(d => d.IdUser)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbDcontact_tbUser");
//            });

//            modelBuilder.Entity<TbLogin>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToTable("tbLogin");

//                entity.Property(e => e.Password)
//                    .IsUnicode(false)
//                    .HasColumnName("password");

//                entity.Property(e => e.Username)
//                    .IsUnicode(false)
//                    .HasColumnName("username");
//            });

//            modelBuilder.Entity<TbOrderInformation>(entity =>
//            {
//                entity.HasKey(e => e.TradingCode);

//                entity.ToTable("tbOrderInformation");

//                entity.Property(e => e.TradingCode)
//                    .HasMaxLength(450)
//                    .IsUnicode(false);

//                entity.Property(e => e.Address)
//                    .IsUnicode(true)
//                    .HasColumnName("address");

//                entity.Property(e => e.ExportPrice)
//                    .HasColumnType("money")
//                    .HasColumnName("exportPrice");

//                entity.Property(e => e.IdUser)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_user");

//                entity.Property(e => e.Phone)
//                    .IsUnicode(false)
//                    .HasColumnName("phone");

//                entity.Property(e => e.PitureLocation)
//                    .IsUnicode(false)
//                    .HasColumnName("pitureLocation");

//                entity.HasOne(d => d.IdUserNavigation)
//                    .WithMany(p => p.TbOrderInformations)
//                    .HasForeignKey(d => d.IdUser)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbOrderInformation_tbUser1");
//            });

//            modelBuilder.Entity<TbReport>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToTable("tbReport");

//                entity.Property(e => e.Description)
//                    .IsUnicode(true)
//                    .HasColumnName("description");

//                entity.Property(e => e.IdRow)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_row");

//                entity.Property(e => e.IdUser)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_user");

//                entity.Property(e => e.Status).HasColumnName("status");

//                entity.HasOne(d => d.IdRowNavigation)
//                    .WithMany()
//                    .HasForeignKey(d => d.IdRow)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbReport_tbRowContent");

//                entity.HasOne(d => d.IdUserNavigation)
//                    .WithMany()
//                    .HasForeignKey(d => d.IdUser)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbReport_tbUser");
//            });

//            modelBuilder.Entity<TbRowContent>(entity =>
//            {
//                entity.ToTable("tbRowContent");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.Birth)
//                    .HasColumnType("date")
//                    .HasColumnName("birth");

//                entity.Property(e => e.Click).HasColumnName("click");

//                entity.Property(e => e.Code)
//                    .IsUnicode(false)
//                    .HasColumnName("code");

//                entity.Property(e => e.IdRowDesign)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_RowDesign");

//                entity.Property(e => e.Link)
//                    .IsUnicode(false)
//                    .HasColumnName("link");

//                entity.Property(e => e.Text)
//                    .IsUnicode(true)
//                    .HasColumnName("text");

//                entity.HasOne(d => d.IdRowDesignNavigation)
//                    .WithMany(p => p.TbRowContents)
//                    .HasForeignKey(d => d.IdRowDesign)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbRowContent_tbRowDesign");
//            });

//            modelBuilder.Entity<TbRowDesign>(entity =>
//            {
//                entity.ToTable("tbRowDesign");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.Bullet)
//                    .IsUnicode(false)
//                    .HasColumnName("bullet");

//                entity.Property(e => e.Font)
//                    .IsUnicode(false)
//                    .HasColumnName("font");

//                entity.Property(e => e.IdTemplate)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_Template");

//                entity.Property(e => e.RowColor)
//                    .IsUnicode(false)
//                    .HasColumnName("rowColor");

//                entity.HasOne(d => d.IdTemplateNavigation)
//                    .WithMany(p => p.TbRowDesigns)
//                    .HasForeignKey(d => d.IdTemplate)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbRowDesign_tbTemplate1");
//            });

//            modelBuilder.Entity<TbTemplate>(entity =>
//            {
//                entity.ToTable("tbTemplate");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.IdAvatar)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_Avatar");

//                entity.Property(e => e.IdBackGround)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_BackGround");

//                entity.Property(e => e.IdDcontact)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID_Dcontact");

//                entity.Property(e => e.IsApply).HasColumnName("isApply");

//                entity.HasOne(d => d.IdAvatarNavigation)
//                    .WithMany(p => p.TbTemplates)
//                    .HasForeignKey(d => d.IdAvatar)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbTemplate_tbAvatar");

//                entity.HasOne(d => d.IdBackGroundNavigation)
//                    .WithMany(p => p.TbTemplates)
//                    .HasForeignKey(d => d.IdBackGround)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbTemplate_tbBackGround");

//                entity.HasOne(d => d.IdDcontactNavigation)
//                    .WithMany(p => p.TbTemplates)
//                    .HasForeignKey(d => d.IdDcontact)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbTemplate_tbDcontact");
//            });

//            modelBuilder.Entity<TbUser>(entity =>
//            {
//                entity.ToTable("tbUser");

//                entity.Property(e => e.Id)
//                    .HasMaxLength(450)
//                    .IsUnicode(true)
//                    .HasColumnName("ID");

//                entity.Property(e => e.Email)
//                    .IsUnicode(false)
//                    .HasColumnName("email");

//                entity.Property(e => e.IsBan).HasColumnName("isBan");

//                entity.HasOne(d => d.IdNavigation)
//                    .WithOne(p => p.TbUser)
//                    .HasForeignKey<TbUser>(d => d.Id)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_tbUser_tbAccount");
//            });

//            modelBuilder.Entity<TbVerifyCode>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToTable("tbVerifyCode");

//                entity.Property(e => e.Code)
//                    .HasMaxLength(6)
//                    .IsUnicode(false)
//                    .HasColumnName("code");

//                entity.Property(e => e.Email)
//                    .IsUnicode(false)
//                    .HasColumnName("email");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
