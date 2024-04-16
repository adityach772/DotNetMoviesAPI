using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MoviesAPI.Models;

namespace MoviesAPI.Data
{
    public partial class searchmoviesContext : DbContext
    {

        public searchmoviesContext(DbContextOptions<searchmoviesContext> options)
            : base(options)
        {
        }

        // Define DbSet for the 'registration' table
        public virtual DbSet<Registration> Registrations { get; set; } = null!;

        // Define DbSet for the 'FavMovie' table
        public DbSet<FavMovie> FavMovies { get; set; }

        // Configure model relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(e => e.Uname)
                    .HasName("PK__registra__4E9EA486F3867445");

                entity.ToTable("registration");

                entity.Property(e => e.Uname)
                    .HasMaxLength(30)
                    .HasColumnName("UNAME");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Passwrd)
                    .HasMaxLength(30)
                    .HasColumnName("passwrd");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    }
}
