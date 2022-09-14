using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dcontact.Data
{
    public partial class Web_ProjectContext : IdentityDbContext<UserIdentity>
    {
        public Web_ProjectContext()
        {
        }

        public Web_ProjectContext(DbContextOptions<Web_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserIdentity> UserIdentity { get; set; } = null!;
        public virtual DbSet<TbAvatar> TbAvatars { get; set; } = null!;
        public virtual DbSet<TbBackGround> TbBackGrounds { get; set; } = null!;
        public virtual DbSet<TbDcontact> TbDcontacts { get; set; } = null!;
        public virtual DbSet<TbOrderInformation> TbOrderInformations { get; set; } = null!;
        public virtual DbSet<TbReport> TbReports { get; set; } = null!;
        public virtual DbSet<TbRowContent> TbRowContents { get; set; } = null!;
        public virtual DbSet<TbRowDesign> TbRowDesigns { get; set; } = null!;
        public virtual DbSet<TbTemplate> TbTemplates { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = Web_Project; User ID = sa; Password=123456 ; integrated security = True; Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TbAvatar>(entity =>
            {
                entity.ToTable("tbAvatar");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PictureLocation)
                    .IsUnicode(false)
                    .HasColumnName("pictureLocation");
            });

            modelBuilder.Entity<TbBackGround>(entity =>
            {
                entity.ToTable("tbBackGround");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PictureLocation)
                    .IsUnicode(false)
                    .HasColumnName("pictureLocation");
            });

            modelBuilder.Entity<TbDcontact>(entity =>
            {
                entity.ToTable("tbDcontact");

                entity.HasIndex(e => e.IdUser, "IX_tbDcontact_ID_User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.HasOne(d => d.IdUserIdentityNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbDcontact_tbUser");
            });


            modelBuilder.Entity<TbOrderInformation>(entity =>
            {
                entity.HasKey(e => e.TradingCode);

                entity.ToTable("tbOrderInformation");

                entity.Property(e => e.Address)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.ExportPrice)
                    .HasColumnType("money")
                    .HasColumnName("exportPrice");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(450)
                    .HasColumnName("ID_user");

                entity.Property(e => e.Phone)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.PitureLocation)
                    .IsUnicode(false)
                    .HasColumnName("pitureLocation");

                entity.HasOne(d => d.IdUserIdentityNavigation)
                   .WithMany(p => p.TbOrderInformations)
                   .HasForeignKey(d => d.IdUser)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_tbOrderInformation_AspNetUsers");
            });

            modelBuilder.Entity<TbReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbReport");

                entity.HasIndex(e => e.IdRow, "IX_tbReport_ID_row");

                entity.HasIndex(e => e.IdUser, "IX_tbReport_ID_user");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IdRow).HasColumnName("ID_row");

                entity.Property(e => e.IdUser).HasColumnName("ID_user");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdRowNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdRow)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbReport_tbRowContent");

                entity.HasOne(d => d.IdUserIdentityNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbReport_AspNetUsers");
            });

            modelBuilder.Entity<TbRowContent>(entity =>
            {
                entity.ToTable("tbRowContent");

                entity.HasIndex(e => e.IdRowDesign, "IX_tbRowContent_ID_RowDesign");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birth)
                    .HasColumnType("date")
                    .HasColumnName("birth");

                entity.Property(e => e.Click).HasColumnName("click");

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.IdRowDesign).HasColumnName("ID_RowDesign");

                entity.Property(e => e.Link)
                    .IsUnicode(false)
                    .HasColumnName("link");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.HasOne(d => d.IdRowDesignNavigation)
                    .WithMany(p => p.TbRowContents)
                    .HasForeignKey(d => d.IdRowDesign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbRowContent_tbRowDesign");
            });

            modelBuilder.Entity<TbRowDesign>(entity =>
            {
                entity.ToTable("tbRowDesign");

                entity.HasIndex(e => e.IdTemplate, "IX_tbRowDesign_ID_Template");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bullet)
                    .IsUnicode(false)
                    .HasColumnName("bullet");

                entity.Property(e => e.Font)
                    .IsUnicode(false)
                    .HasColumnName("font");

                entity.Property(e => e.IdTemplate).HasColumnName("ID_Template");

                entity.Property(e => e.RowColor)
                    .IsUnicode(false)
                    .HasColumnName("rowColor");

                entity.HasOne(d => d.IdTemplateNavigation)
                    .WithMany(p => p.TbRowDesigns)
                    .HasForeignKey(d => d.IdTemplate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbRowDesign_tbTemplate1");
            });

            modelBuilder.Entity<TbTemplate>(entity =>
            {
                entity.ToTable("tbTemplate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdAvatar)
                    .HasMaxLength(450)
                    .HasColumnName("ID_Avatar");

                entity.Property(e => e.IdBackGround)
                    .HasMaxLength(450)
                    .HasColumnName("ID_BackGround");

                entity.Property(e => e.IdDcontact)
                    .HasMaxLength(450)
                    .HasColumnName("ID_Dcontact");

                entity.Property(e => e.IsApply).HasColumnName("isApply");

                entity.HasOne(d => d.IdAvatarNavigation)
                    .WithMany(p => p.TbTemplates)
                    .HasForeignKey(d => d.IdAvatar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbTemplate_tbAvatar");

                entity.HasOne(d => d.IdBackGroundNavigation)
                    .WithMany(p => p.TbTemplates)
                    .HasForeignKey(d => d.IdBackGround)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbTemplate_tbBackGround");

                entity.HasOne(d => d.IdDcontactNavigation)
                    .WithMany(p => p.TbTemplates)
                    .HasForeignKey(d => d.IdDcontact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbTemplate_tbDcontact");
            });

            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public TbDcontact CreateTbDcontact(string id)
        {
            return new TbDcontact()
            {
                Id = Guid.NewGuid().ToString(),
                IdUser = id
            };
        }
    }
}
