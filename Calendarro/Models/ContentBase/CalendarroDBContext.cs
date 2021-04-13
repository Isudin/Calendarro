using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class CalendarroDBContext : DbContext
    {
        public CalendarroDBContext()
        {
        }

        public CalendarroDBContext(DbContextOptions<CalendarroDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kanban> Kanbans { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<ProjectUserRelation> ProjectUserRelations { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Kanban>(entity =>
            {
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

            modelBuilder.Entity<Project>(entity =>
            {
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

            modelBuilder.Entity<ProjectTask>(entity =>
            {
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

                entity.ToTable("ProjectUserRelation");

                entity.Property(e => e.LinkId).HasColumnName("LinkID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectUserRelations)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUserRelation_Projects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectUserRelations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUserRelation_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("E_mail");

                entity.Property(e => e.HouseNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
