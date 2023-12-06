using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaTecnica.Models
{
    public partial class TrabajadoresPruebaContext : DbContext
    {
        public TrabajadoresPruebaContext()
        {
        }

        public TrabajadoresPruebaContext(DbContextOptions<TrabajadoresPruebaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Distrito> Distritos { get; set; } = null!;
        public virtual DbSet<Provincium> Provincia { get; set; } = null!;
        public virtual DbSet<Trabajadore> Trabajadores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
         //      optionsBuilder.UseSqlServer("server=DESKTOP-IRVD2U9\\SQLEXPRESS; database=TrabajadoresPrueba; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.ToTable("Distrito");

                entity.Property(e => e.NombreDistrito)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Distritos)
                    .HasForeignKey(d => d.IdProvincia)
                    .HasConstraintName("FK__Distrito__IdProv__4F7CD00D");
            });

            modelBuilder.Entity<Provincium>(entity =>
            {
                entity.Property(e => e.NombreProvincia)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__Provincia__IdDep__5070F446");
            });

            modelBuilder.Entity<Trabajadore>(entity =>
            {
                entity.Property(e => e.Nombres)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__Trabajado__IdDep__5165187F");

                entity.HasOne(d => d.IdDistritoNavigation)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdDistrito)
                    .HasConstraintName("FK__Trabajado__IdDis__52593CB8");

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdProvincia)
                    .HasConstraintName("FK__Trabajado__IdPro__534D60F1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
