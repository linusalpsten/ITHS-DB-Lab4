using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITHS_DB_Lab4
{
    class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<SessionExercise> SessionExercises { get; set; }
        public DbSet<SessionEquipment> SessionEquipment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ITHS-DB-Lab4;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionExercise>().HasKey(se => new { se.SessionId, se.ExerciseId });
            modelBuilder.Entity<SessionEquipment>().HasKey(se => new { se.SessionId, se.EquipmentId });
        }
    }
}
