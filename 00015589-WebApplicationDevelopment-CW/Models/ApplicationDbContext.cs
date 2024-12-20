﻿using Microsoft.EntityFrameworkCore;

namespace _00015589_WebApplicationDevelopment_CW.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<MovieModel> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
