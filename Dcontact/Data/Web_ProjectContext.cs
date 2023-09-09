using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
                optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = Web_Project; User ID = sa; Password=123456 ; integrated security = True; Encrypt=False");
            }
            optionsBuilder.UseLazyLoadingProxies(true);

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

                entity.Property(e => e.view).HasColumnName("View").HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdUserNavigation)
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
                entity.ToTable("tbReport");

                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.IdRow, "IX_tbReport_ID_row");

                entity.HasIndex(e => e.IdUser, "IX_tbReport_ID_user");

                entity.Property(e => e.Id).HasDefaultValueSql("(N'')");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IdRow).HasColumnName("ID_row");

                entity.Property(e => e.IdUser).HasColumnName("ID_user");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdRowNavigation)
                    .WithMany(p => p.TbReports)
                    .HasForeignKey(d => d.IdRow)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbReport_tbRowContent");

                entity.HasOne(d => d.IdUserIdentityNavigation)
                    .WithMany(p => p.TbReports)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbReport_AspNetUsers");
            });

            modelBuilder.Entity<TbRowContent>(entity =>
            {
                entity.ToTable("tbRowContent");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birth)
                    .HasColumnType("date")
                    .HasColumnName("birth");

                entity.Property(e => e.Click).HasColumnName("click");

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.IdDcontact)
                    .HasMaxLength(450)
                    .HasColumnName("ID_Dcontact");

                entity.Property(e => e.Link)
                    .IsUnicode(false)
                    .HasColumnName("link");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.HasOne(d => d.IdDcontactNavigation)
                    .WithMany(p => p.TbRowContents)
                    .HasForeignKey(d => d.IdDcontact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbRowContent_tbDcontact");
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

                entity.Property(e => e.IdRowContent)
                    .HasMaxLength(450)
                    .HasColumnName("ID_RowContent");

                entity.Property(e => e.IdTemplate).HasColumnName("ID_Template");

                entity.Property(e => e.RowColor)
                    .IsUnicode(false)
                    .HasColumnName("rowColor");

                entity.HasOne(d => d.IdRowContentNavigation)
                    .WithMany(p => p.TbRowDesigns)
                    .HasForeignKey(d => d.IdRowContent)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbRowDesign_tbRowContent");

                entity.HasOne(d => d.IdTemplateNavigation)
                    .WithMany(p => p.TbRowDesigns)
                    .HasForeignKey(d => d.IdTemplate)
                    .OnDelete(DeleteBehavior.Cascade)
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

                entity.Property(e => e.Name)
                      .HasColumnName("Name");

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

        public void CreateDcontact(UserIdentity user)
        {

            var d = this.TbDcontacts.FirstOrDefault(x => x.IdUser == user.Id);
            if (d != null)
            {
                throw new Exception($"Can't create a dcontact for user {user.Id}, user's dcontact has been exist!");
            }

            var Dcontact = new TbDcontact()
            {
                Id = Guid.NewGuid().ToString(),
                IdUser = user.Id
            };

            this.TbDcontacts.Add(Dcontact);
            this.SaveChanges();

            var temp = this.TbTemplates.FirstOrDefault(x => x.IdDcontact == Dcontact.Id && x.IsApply == true);
            if (temp != null)
            {
                temp.IsApply = false;
                this.Update(temp);
                this.SaveChanges();
            }
            var Template = new TbTemplate()
            {
                Id = Guid.NewGuid().ToString(),
                IdDcontact = Dcontact.Id,
                IdAvatar = "default",
                IdBackGround = "default",
                Name = "Template Default",
                IsApply = true
            };
            this.TbTemplates.Add(Template);
            this.SaveChanges();

            createRow(Dcontact.Id);

        }

        public TbTemplate CreateTemplate(string idDcontact, string Name)
        {
            var content = this.TbRowContents.Where(f => f.IdDcontact == idDcontact).ToList();

            var template = new TbTemplate()
            {
                Id = Guid.NewGuid().ToString(),
                IdDcontact = idDcontact,
                IdAvatar = Default.ID_TEMPLATE_AVATAR,
                IdBackGround = Default.ID_TEMPLATE_BACKGROUND,
                Name = Name,
                IsApply = false
            };


            this.TbTemplates.Add(template);
            this.SaveChanges();
            foreach (var contents in content)
            {
                AddRowDesign(template.Id, contents.Id);
            }
            return ApplyTemplate(idDcontact, template.Id);

        }

        public TbTemplate ApplyTemplate(string idDcontact, string idTemplate)
        {
            var temp = this.TbTemplates.FirstOrDefault(x => x.IdDcontact == idDcontact && x.IsApply == true);
            if (temp != null)
            {
                temp.IsApply = false;
                this.Update(temp);
                //this.SaveChanges();
            }

            var tempApply = this.TbTemplates.FirstOrDefault(t => t.Id == idTemplate);
            tempApply.IsApply = true;
            this.Update(tempApply);
            this.SaveChanges();
            return tempApply;
        }

        public TbRowContent createRow(string idDcontact)
        {
            var templates = this.TbTemplates.Where(e => e.IdDcontact == idDcontact).ToList();
            TbRowContent ct = new TbRowContent()
            {
                Id = Guid.NewGuid().ToString(),
                IdDcontact = idDcontact,
                Text = Default.ROW_TEXT,
                Link = Default.ROW_LINK,
                Code = null,
                Birth = null,
                Click = Default.ROW_CLICK
            };
            this.TbRowContents.Add(ct);
            this.SaveChanges();
            foreach (var temp in templates)
            {
                AddRowDesign(temp.Id, ct.Id);
            }
            return ct;
        }

        public void AddRowDesign(string idTemplate, string idRowContent)
        {
            var rowDesign = new TbRowDesign()
            {
                Id = Guid.NewGuid().ToString(),
                IdRowContent = idRowContent,
                IdTemplate = idTemplate,
                Font = Default.ROW_FONT,
                RowColor = Default.ROW_COLOR,
                Bullet = Default.ROW_BULLET
            };
            this.TbRowDesigns.Add(rowDesign);
            this.SaveChanges();
        }

        public bool UpdateRow(string idContent, string idDcontact, string text, string link, string font, string color, string? code, string? birth, string? bullet)
        {
            var content = this.TbRowContents.FirstOrDefault(e => e.Id == idContent);
            var template = this.TbTemplates.FirstOrDefault(t => t.IdDcontact == idDcontact && t.IsApply == true);
            var design = this.TbRowDesigns.FirstOrDefault(d => d.IdRowContent == content.Id && d.IdTemplate == template.Id);
            if (content == null || design == null)
            {
                return false;
            }
            content.Link = link;
            content.Text = text;
            this.Update(content);

            design.Font = font;
            design.RowColor = color;
            this.Update(design);
            if (code != null)
            {
                content.Code = code;
            }
            if (birth != null)
            {
                content.Birth = DateTime.Parse(birth);
            }
            if (bullet != null)
            {
                design.Bullet = bullet;
            }
            this.SaveChanges();
            return true;
        }

        public bool UpdateBackground(string idDcontact, string imgBg, string idTemplate)
        {
            var template = this.TbTemplates.FirstOrDefault(t => t.IdDcontact == idDcontact && t.Id == idTemplate);
  
            var tbBackground = new TbBackGround()
            {
                Id = Guid.NewGuid().ToString(),
                PictureLocation = imgBg
            };

            this.TbBackGrounds.Add(tbBackground);
            template.IdBackGround = tbBackground.Id;
            this.Update(template);

            this.SaveChanges();
            return true;
        }

        public bool UpdateAvatar(string idDcontact, string imgAvt, string idTemplate)
        {
            var template = this.TbTemplates.FirstOrDefault(t => t.IdDcontact == idDcontact && t.Id == idTemplate);

            var tbAvatar = new TbAvatar()
            {
                Id = Guid.NewGuid().ToString(),
                PictureLocation= imgAvt
            };

            this.TbAvatars.Add(tbAvatar);
            template.IdAvatar = tbAvatar.Id;
            this.Update(template);

            this.SaveChanges();
            return true;
        }

        public void InsertPayment(string tradingCode,string userid, string address, string phone)
        {
            TbOrderInformation ord = new TbOrderInformation()
            {
                TradingCode = tradingCode,
                IdUser = userid,
                Address = address,
                Phone = phone,
                ExportPrice = 50000,
                PitureLocation = "abcabc"
            };
            this.TbOrderInformations.Add(ord);
            this.SaveChanges();
        }

    }
}
