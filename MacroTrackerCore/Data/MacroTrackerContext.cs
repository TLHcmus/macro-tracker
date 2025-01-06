﻿using System;
using System.Collections.Generic;
using MacroTrackerCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace MacroTrackerCore.Data;

/// <summary>
/// Represents the database context for the MacroTracker application.
/// </summary>
public partial class MacroTrackerContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MacroTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public MacroTrackerContext(DbContextOptions<MacroTrackerContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Exercises DbSet.
    /// </summary>
    public virtual DbSet<Exercise> Exercises { get; set; }

    /// <summary>
    /// Gets or sets the Foods DbSet.
    /// </summary>
    public virtual DbSet<Food> Foods { get; set; }

    /// <summary>
    /// Gets or sets the Goals DbSet.
    /// </summary>
    public virtual DbSet<Goal> Goals { get; set; }

    /// <summary>
    /// Gets or sets the Logs DbSet.
    /// </summary>
    public virtual DbSet<Log> Logs { get; set; }

    /// <summary>
    /// Gets or sets the LogExerciseItems DbSet.
    /// </summary>
    public virtual DbSet<LogExerciseItem> LogExerciseItems { get; set; }

    /// <summary>
    /// Gets or sets the LogFoodItems DbSet.
    /// </summary>
    public virtual DbSet<LogFoodItem> LogFoodItems { get; set; }

    /// <summary>
    /// Gets or sets the Users DbSet.
    /// </summary>
    public virtual DbSet<User> Users { get; set; }

    /// <summary>
    /// Configures the schema needed for the context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        ConfigureExerciseEntity(modelBuilder);
        ConfigureFoodEntity(modelBuilder);
        ConfigureGoalEntity(modelBuilder);
        ConfigureLogEntity(modelBuilder);
        ConfigureLogExerciseItemEntity(modelBuilder);
        ConfigureLogFoodItemEntity(modelBuilder);
        ConfigureUserEntity(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    /// <summary>
    /// Configures the Exercise entity.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    private static void ConfigureExerciseEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PRIMARY");

            entity.ToTable("exercises");

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.CaloriesPerMinute).HasColumnName("calories_per_minute");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name")
                .HasCharSet("utf8mb3");
        });
    }

    /// <summary>
    /// Configures the Food entity.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    private static void ConfigureFoodEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PRIMARY");

            entity.ToTable("foods");

            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.CaloriesPer100g).HasColumnName("calories_per_100g");
            entity.Property(e => e.CarbsPer100g).HasColumnName("carbs_per_100g");
            entity.Property(e => e.FatPer100g).HasColumnName("fat_per_100g");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProteinPer100g).HasColumnName("protein_per_100g");
        });
    }

    /// <summary>
    /// Configures the Goal entity.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    private static void ConfigureGoalEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("PRIMARY");

            entity.ToTable("goal");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.GoalId).HasColumnName("goal_id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbs).HasColumnName("carbs");
            entity.Property(e => e.Fat).HasColumnName("fat");
            entity.Property(e => e.Protein).HasColumnName("protein");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Goals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("goal_ibfk_1");
        });
    }

    /// <summary>
    /// Configures the Log entity.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    private static void ConfigureLogEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.ToTable("logs");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.LogDate).HasColumnName("log_date");
            entity.Property(e => e.TotalCalories)
                .HasDefaultValueSql("'0'")
                .HasColumnName("total_calories");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("logs_ibfk_1");
        });
    }

    /// <summary>
    /// Configures the LogExerciseItem entity.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    private static void ConfigureLogExerciseItemEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogExerciseItem>(entity =>
        {
            entity.HasKey(e => e.LogExerciseId).HasName("PRIMARY");

            entity.ToTable("log_exercise_items");

            entity.HasIndex(e => e.ExerciseId, "exercise_id");

            entity.HasIndex(e => e.LogId, "log_id");

            entity.Property(e => e.LogExerciseId).HasColumnName("log_exercise_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.TotalCalories).HasColumnName("total_calories");

            entity.HasOne(d => d.Exercise).WithMany(p => p.LogExerciseItems)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("log_exercise_items_ibfk_2");

            entity.HasOne(d => d.Log).WithMany(p => p.LogExerciseItems)
                .HasForeignKey(d => d.LogId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("log_exercise_items_ibfk_1");
        });
    }

    /// <summary>
    /// Configures the LogFoodItem entity.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    private static void ConfigureLogFoodItemEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogFoodItem>(entity =>
        {
            entity.HasKey(e => e.LogFoodId).HasName("PRIMARY");

            entity.ToTable("log_food_items");

            entity.HasIndex(e => e.FoodId, "food_id");

            entity.HasIndex(e => e.LogId, "log_id");

            entity.Property(e => e.LogFoodId).HasColumnName("log_food_id");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.NumberOfServings).HasColumnName("number_of_servings");
            entity.Property(e => e.TotalCalories).HasColumnName("total_calories");

            entity.HasOne(d => d.Food).WithMany(p => p.LogFoodItems)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("log_food_items_ibfk_2");

            entity.HasOne(d => d.Log).WithMany(p => p.LogFoodItems)
                .HasForeignKey(d => d.LogId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("log_food_items_ibfk_1");
        });
    }

    /// <summary>
    /// Configures the User entity.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    private static void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.EncryptedPassword)
                .HasMaxLength(255)
                .HasColumnName("encrypted_password")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username")
                .HasCharSet("utf8mb3");
        });
    }

    /// <summary>
    /// A partial method that can be used to configure the model further.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
