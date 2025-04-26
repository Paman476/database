using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SMS.Models;

public partial class ManagementSystemContext : DbContext
{
    public ManagementSystemContext()
    {
    }

    public ManagementSystemContext(DbContextOptions<ManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=ep-wandering-smoke-a4yjno04-pooler.us-east-1.aws.neon.tech;Database=ManagementSystem;Username=ManagementSystem_owner;Password=npg_SPmHxc4R5wzA");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CCode).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.CCode)
                .HasMaxLength(10)
                .HasColumnName("c_code");
            entity.Property(e => e.CName)
                .HasMaxLength(150)
                .HasColumnName("c_name");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .HasColumnName("duration");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Rid).HasName("registration_pkey");

            entity.ToTable("registration");

            entity.Property(e => e.Rid).HasColumnName("rid");
            entity.Property(e => e.Ccode)
                .HasMaxLength(10)
                .HasColumnName("ccode");
            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.Tid).HasColumnName("tid");

            entity.HasOne(d => d.CcodeNavigation).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.Ccode)
                .HasConstraintName("registration_ccode_fkey");

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.Sid)
                .HasConstraintName("registration_sid_fkey");

            entity.HasOne(d => d.TidNavigation).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.Tid)
                .HasConstraintName("registration_tid_fkey");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("student_pkey");

            entity.ToTable("student");

            entity.HasIndex(e => e.Contact, "student_contact_key").IsUnique();

            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.Contact)
                .HasMaxLength(13)
                .HasColumnName("contact");
            entity.Property(e => e.Sname)
                .HasMaxLength(28)
                .HasColumnName("sname");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Tid).HasName("teacher_pkey");

            entity.ToTable("teacher");

            entity.Property(e => e.Tid).HasColumnName("tid");
            entity.Property(e => e.Tname)
                .HasMaxLength(28)
                .HasColumnName("tname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
