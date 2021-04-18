using System;
using Calendarro.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Calendarro.Models.Database
{
    public partial class CalendarroDBContext : IdentityDbContext<CalendarroUser>
    {
        public CalendarroDBContext()
        {
        }

        public CalendarroDBContext(DbContextOptions<CalendarroDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kanbans> Kanbans { get; set; }
        public virtual DbSet<ProjectTasks> ProjectTasks { get; set; }
        public virtual DbSet<ProjectUserRelation> ProjectUserRelation { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<CalendarroUsers> CalendarroUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //                optionsBuilder.UseSqlServer("Data Source=DESKTOP-6OR0K4V;Initial Catalog=CalendarroDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kanbans>(entity =>
            {
                entity.HasKey(e => e.KanbanId)
                    .HasName("PK_Kanban");

                entity.Property(e => e.KanbanId).HasColumnName("KanbanID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Kanbans)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kanbans_Projects");
            });

            modelBuilder.Entity<ProjectTasks>(entity =>
            {
                entity.HasKey(e => e.ProjectTaskId);

                entity.Property(e => e.ProjectTaskId).HasColumnName("ProjectTaskID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.KanbanId).HasColumnName("KanbanID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Kanban)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.KanbanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTasks_Kanbans");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTasks_Projects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTasks_Users");
            });

            modelBuilder.Entity<ProjectUserRelation>(entity =>
            {
                entity.HasKey(e => e.LinkId)
                    .HasName("PK_Link");

                entity.Property(e => e.LinkId).HasColumnName("LinkID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectUserRelation)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUserRelation_Projects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectUserRelation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUserRelation_Users");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatorId).HasColumnName("CreatorID");

                entity.Property(e => e.FinishingDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projects_Users");
            });

            modelBuilder.Entity<CalendarroUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("E_mail")
                    .HasMaxLength(50);

                entity.Property(e => e.HouseNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
