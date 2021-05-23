using Microsoft.EntityFrameworkCore;
using System;
using list.data.Models;

namespace list.data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=db;Database=msdb;User Id=SA;Password='Atlo2DrP1';Trusted_Connection=False;");
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.IsCompleted)
                    .HasColumnName("isCompleted")
                    .IsRequired();
            });
        }
    }
}
