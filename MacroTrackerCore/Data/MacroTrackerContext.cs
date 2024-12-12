using System;
using System.Collections.Generic;
using MacroTrackerCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace MacroTrackerCore.Data;

public partial class MacroTrackerContext : DbContext
{
    public MacroTrackerContext(DbContextOptions<MacroTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PRIMARY");

            entity.ToTable("exercises");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IconFileName)
                .HasMaxLength(255)
                .HasColumnName("icon_file_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PRIMARY");

            entity.ToTable("foods");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CaloriesPer100g).HasColumnName("calories_per_100g");
            entity.Property(e => e.CarbsPer100g).HasColumnName("carbs_per_100g");
            entity.Property(e => e.FatPer100g).HasColumnName("fat_per_100g");
            entity.Property(e => e.ProteinPer100g).HasColumnName("protein_per_100g");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("goal");

            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbs).HasColumnName("carbs");
            entity.Property(e => e.Fat).HasColumnName("fat");
            entity.Property(e => e.Protein).HasColumnName("protein");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Username)
                .HasColumnName("username")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.EncryptedPassword)
                .HasMaxLength(255)
                .HasColumnName("encrypted_password")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
