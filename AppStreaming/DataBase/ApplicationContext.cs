using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Serie> Series { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<SerieGenero> SerieGeneros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            #region Tables
            modelBuilder.Entity<Serie>().ToTable("Series");
            modelBuilder.Entity<Producer>().ToTable("Producers");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<SerieGenero>().ToTable("SerieGeneros");
            #endregion
            #region "Primary Keys"
            modelBuilder.Entity<Serie>().HasKey(Serie => Serie.Id);
            modelBuilder.Entity<Producer>().HasKey(Producer => Producer.Id);
            modelBuilder.Entity<Genre>().HasKey(Genre => Genre.Id);
            modelBuilder.Entity<SerieGenero>().HasKey(x => new { x.SerieId, x.GeneroId });
            #endregion
            #region Relationships
            modelBuilder.Entity<Producer>()
                .HasMany<Serie>(p => p.Series)
                .WithOne(s => s.Producer)
                .HasForeignKey(s => s.ProductoraId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SerieGenero>()
                .HasOne<Serie>(sg => sg.Serie)
                .WithMany(s => s.SerieGeneros)
                .HasForeignKey(sg => sg.SerieId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SerieGenero>()
                .HasOne<Genre>(sg => sg.Genero)
                .WithMany(g => g.SerieGeneros)
                .HasForeignKey(sg => sg.GeneroId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "property configuration"
            #region Series
            modelBuilder.Entity<Serie>()
                .Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Serie>()
                .Property(w => w.Description)
                .IsRequired();
            modelBuilder.Entity<Serie>()
                .Property(r => r.VideoYout)
                .IsRequired();
            #endregion 
            #region Producer
            modelBuilder.Entity<Producer>()
                .Property(p => p.Name)
                .IsRequired();
            #endregion
            #region Genero
            modelBuilder.Entity<Genre>()
                .Property(y => y.Name)
                .IsRequired();
            #endregion

            #endregion
        }
    }
}