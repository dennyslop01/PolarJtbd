using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Jtbd.Infrastructure.Models;

public partial class PolarJtbdContext : DbContext
{
    public PolarJtbdContext()
    {
    }

    public PolarJtbdContext(DbContextOptions<PolarJtbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anxiety> Anxieties { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Deparment> Deparments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Habit> Habits { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<PullGroup> PullGroups { get; set; }

    public virtual DbSet<PushesGroup> PushesGroups { get; set; }

    public virtual DbSet<StoriesAnxiety> StoriesAnxieties { get; set; }

    public virtual DbSet<StoriesHabbit> StoriesHabbits { get; set; }

    public virtual DbSet<StoriesPull> StoriesPulls { get; set; }

    public virtual DbSet<StoriesPush> StoriesPushes { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQL2K19;Initial Catalog=PolarJTBD;User ID=sa;Password=123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anxiety>(entity =>
        {
            entity.HasKey(e => e.IdAnxie);

            entity.HasIndex(e => e.ProjectIdProject, "IX_Anxieties_ProjectIdProject");

            entity.Property(e => e.AnxieName).HasMaxLength(100);

            entity.HasOne(d => d.ProjectIdProjectNavigation).WithMany(p => p.Anxieties).HasForeignKey(d => d.ProjectIdProject);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Deparment>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.DeparmentsId, "IX_Employees_DeparmentsId");

            entity.Property(e => e.EmployeeName).HasMaxLength(500);

            entity.HasOne(d => d.Deparments).WithMany(p => p.Employees).HasForeignKey(d => d.DeparmentsId);
        });

        modelBuilder.Entity<Habit>(entity =>
        {
            entity.HasKey(e => e.IdHabit);

            entity.HasIndex(e => e.ProjectIdProject, "IX_Habits_ProjectIdProject");

            entity.Property(e => e.HabitName).HasMaxLength(100);

            entity.HasOne(d => d.ProjectIdProjectNavigation).WithMany(p => p.Habits).HasForeignKey(d => d.ProjectIdProject);
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => e.IdInter);

            entity.Property(e => e.InterName).HasMaxLength(100);
            entity.Property(e => e.InterNickname).HasMaxLength(100);
            entity.Property(e => e.InterNse)
                .HasMaxLength(3)
                .HasColumnName("InterNSE");
            entity.Property(e => e.InterOccupation).HasMaxLength(100);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject);

            entity.HasIndex(e => e.CategoriesId, "IX_Projects_CategoriesId");

            entity.HasIndex(e => e.DeparmentId, "IX_Projects_DeparmentId");

            entity.Property(e => e.ProjectDescription).HasMaxLength(1000);
            entity.Property(e => e.ProjectName).HasMaxLength(500);

            entity.HasOne(d => d.Categories).WithMany(p => p.Projects).HasForeignKey(d => d.CategoriesId);

            entity.HasOne(d => d.Deparment).WithMany(p => p.Projects).HasForeignKey(d => d.DeparmentId);
        });

        modelBuilder.Entity<PullGroup>(entity =>
        {
            entity.HasKey(e => e.IdPull);

            entity.HasIndex(e => e.ProjectIdProject, "IX_PullGroups_ProjectIdProject");

            entity.Property(e => e.PullDescription).HasMaxLength(1000);
            entity.Property(e => e.PullName).HasMaxLength(100);

            entity.HasOne(d => d.ProjectIdProjectNavigation).WithMany(p => p.PullGroups).HasForeignKey(d => d.ProjectIdProject);
        });

        modelBuilder.Entity<PushesGroup>(entity =>
        {
            entity.HasKey(e => e.IdPush);

            entity.HasIndex(e => e.ProjectIdProject, "IX_PushesGroups_ProjectIdProject");

            entity.Property(e => e.PushDescription).HasMaxLength(1000);
            entity.Property(e => e.PushName).HasMaxLength(100);

            entity.HasOne(d => d.ProjectIdProjectNavigation).WithMany(p => p.PushesGroups).HasForeignKey(d => d.ProjectIdProject);
        });

        modelBuilder.Entity<StoriesAnxiety>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.IdAnxieNavigation).WithMany()
                .HasForeignKey(d => d.IdAnxie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoriesAnxieties_Anxieties");

            entity.HasOne(d => d.IdStorieNavigation).WithMany()
                .HasForeignKey(d => d.IdStorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoriesAnxieties_Stories");
        });

        modelBuilder.Entity<StoriesHabbit>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.IdHabitNavigation).WithMany()
                .HasForeignKey(d => d.IdHabit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoriesHabbits_Habits");

            entity.HasOne(d => d.IdStorieNavigation).WithMany()
                .HasForeignKey(d => d.IdStorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoriesHabbits_Stories");
        });

        modelBuilder.Entity<StoriesPull>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.IdPullNavigation).WithMany()
                .HasForeignKey(d => d.IdPull)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoriesPulls_PullGroups");

            entity.HasOne(d => d.IdStorieNavigation).WithMany()
                .HasForeignKey(d => d.IdStorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoriesPulls_Stories");
        });

        modelBuilder.Entity<StoriesPush>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.IdPushNavigation).WithMany()
                .HasForeignKey(d => d.IdPush)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoriesPushes_PushesGroups");

            entity.HasOne(d => d.IdStorieNavigation).WithMany()
                .HasForeignKey(d => d.IdStorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoriesPushes_Stories");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.IdStorie);

            entity.HasIndex(e => e.IdInter1, "IX_Stories_IdInter1");

            entity.HasIndex(e => e.ProjectIdProject, "IX_Stories_ProjectIdProject");

            entity.Property(e => e.ContextStorie).HasMaxLength(4000);
            entity.Property(e => e.TitleStorie).HasMaxLength(100);

            entity.HasOne(d => d.IdInter1Navigation).WithMany(p => p.Stories).HasForeignKey(d => d.IdInter1);

            entity.HasOne(d => d.ProjectIdProjectNavigation).WithMany(p => p.Stories).HasForeignKey(d => d.ProjectIdProject);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
