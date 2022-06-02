using System;
using System.Collections.Generic;
using ITS_Middleware.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ITS_Middleware.Models
{
    public partial class MiddlewareDbContext : DbContext
    {
        public MiddlewareDbContext()
        {
        }

        public MiddlewareDbContext(DbContextOptions<MiddlewareDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Proyecto> Proyectos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=it-seekersdev.ddns.net,65535;Database=ITS_OAuth; Uid=its-academy; Pwd=Infinity01?;Encrypt=no;Connection Timeout=120;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.ToTable("proyectos");

                entity.HasIndex(e => e.nombre, "UQ__proyecto__72AFBCC6349FBD46")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.activo).HasColumnName("activo");

                entity.Property(e => e.descripcion)
                    .HasMaxLength(1)
                    .HasColumnName("descripcion");

                entity.Property(e => e.metodoAutenticacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("metodoAutenticacion");

                entity.Property(e => e.nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.password)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("pass");

                entity.Property(e => e.tipoCifrado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipoCifrado");

                entity.Property(e => e.usuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.email, "UQ__usuarios__AB6E6164B23D24B9")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.email)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.fechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.pass)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("pass");

                entity.Property(e => e.puesto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("puesto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
