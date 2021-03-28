using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;

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

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectUserRelation> ProjectUserRelations { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ContenBase"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(e => e.HasTasks, "UC_HT")
                    .IsUnique();

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProjectID");

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
                    .HasConstraintName("FK__CreatorID");
            });

            modelBuilder.Entity<ProjectUserRelation>(entity =>
            {
                entity.HasKey(e => e.LinkId);

                entity.ToTable("ProjectUserRelation");

                entity.Property(e => e.LinkId)
                    .ValueGeneratedNever()
                    .HasColumnName("LinkID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectUserRelations)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectLink_M_to_M");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectUserRelations)
                    .HasPrincipalKey(p => p.BelongsToProjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLink_M_to_M");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.TaskId)
                    .ValueGeneratedNever()
                    .HasColumnName("TaskID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Project");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.BelongsToProjects, "UC_BTP")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("E_mail");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
