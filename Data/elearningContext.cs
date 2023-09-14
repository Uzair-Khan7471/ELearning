using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Data
{
    public partial class elearningContext : DbContext
    {
        public elearningContext()
        {
        }

        public elearningContext(DbContextOptions<elearningContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Batch> Batches { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseAnswer> CourseAnswers { get; set; } = null!;
        public virtual DbSet<CourseLecture> CourseLectures { get; set; } = null!;
        public virtual DbSet<CourseQuestion> CourseQuestions { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Lab> Labs { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=elearning;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batch");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndTime).HasColumnName("End_Time");

                entity.Property(e => e.LabFk).HasColumnName("Lab_FK");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnName("Start_Time");

                entity.Property(e => e.UsersFk).HasColumnName("Users_FK");

                entity.HasOne(d => d.LabFkNavigation)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.LabFk)
                    .HasConstraintName("FK__Batch__Lab_FK__03F0984C");

                entity.HasOne(d => d.UsersFkNavigation)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.UsersFk)
                    .HasConstraintName("FK__Batch__Users_FK__02FC7413");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryFk).HasColumnName("category_FK");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsersFk).HasColumnName("Users_FK");

                entity.HasOne(d => d.CategoryFkNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryFk)
                    .HasConstraintName("FK__Course__category__52593CB8");

                entity.HasOne(d => d.UsersFkNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.UsersFk)
                    .HasConstraintName("FK__Course__Users_FK__04E4BC85");
            });

            modelBuilder.Entity<CourseAnswer>(entity =>
            {
                entity.ToTable("Course_Answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answers).HasColumnType("text");

                entity.Property(e => e.CourseQuestionFk).HasColumnName("Course_Question_FK");

                entity.HasOne(d => d.CourseQuestionFkNavigation)
                    .WithMany(p => p.CourseAnswers)
                    .HasForeignKey(d => d.CourseQuestionFk)
                    .HasConstraintName("FK__Course_An__Cours__5441852A");
            });

            modelBuilder.Entity<CourseLecture>(entity =>
            {
                entity.ToTable("Course_lecture");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseFk).HasColumnName("Course_FK");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Title).HasColumnType("text");

                entity.Property(e => e.Video).IsUnicode(false);

                entity.HasOne(d => d.CourseFkNavigation)
                    .WithMany(p => p.CourseLectures)
                    .HasForeignKey(d => d.CourseFk)
                    .HasConstraintName("FK__Course_le__Cours__5629CD9C");
            });

            modelBuilder.Entity<CourseQuestion>(entity =>
            {
                entity.ToTable("Course_Question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseFk).HasColumnName("Course_FK");

                entity.Property(e => e.Questions).HasColumnType("text");

                entity.HasOne(d => d.CourseFkNavigation)
                    .WithMany(p => p.CourseQuestions)
                    .HasForeignKey(d => d.CourseFk)
                    .HasConstraintName("FK__Course_Qu__Cours__05D8E0BE");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gender1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("gender");
            });

            modelBuilder.Entity<Lab>(entity =>
            {
                entity.ToTable("Lab");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BatchFk).HasColumnName("Batch_FK");

                entity.Property(e => e.ConfirmPassword)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("confirm_password");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.RoleFk).HasColumnName("Role_FK");

                entity.Property(e => e.StatusFk).HasColumnName("Status_FK");

                entity.HasOne(d => d.BatchFkNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.BatchFk)
                    .HasConstraintName("FK__Users__Batch_FK__73BA3083");

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Gender)
                    .HasConstraintName("FK__Users__Gender__71D1E811");

                entity.HasOne(d => d.RoleFkNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleFk)
                    .HasConstraintName("FK__Users__Role_FK__74AE54BC");

                entity.HasOne(d => d.StatusFkNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusFk)
                    .HasConstraintName("FK__Users__Status_FK__72C60C4A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
