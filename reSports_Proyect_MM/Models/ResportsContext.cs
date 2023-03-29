using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace reSports_Proyect_MM.Models;

public partial class ResportsContext : DbContext
{
    public ResportsContext()
    {
    }

    public ResportsContext(DbContextOptions<ResportsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<LogReporte> LogReportes { get; set; }

    public virtual DbSet<Publicacion> Publicacions { get; set; }

    public virtual DbSet<Rolusuario> Rolusuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)

        {

            IConfigurationRoot configuration = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

                        .AddJsonFile("appsettings.json")

                        .Build();

            var connectionString = configuration.GetConnectionString("DBreSports");

            optionsBuilder.UseMySQL(connectionString);

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asistencia");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Asistio).HasColumnName("asistio");
            entity.Property(e => e.FechaAsistencia)
                .HasColumnType("date")
                .HasColumnName("fecha_asistencia");
            entity.Property(e => e.RazonFalta)
                .HasMaxLength(255)
                .HasColumnName("razonFalta");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("asistencia_ibfk_1");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("equipos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Genero).HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<LogReporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("log_reporte");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripccion)
                .HasMaxLength(255)
                .HasColumnName("descripccion");
            entity.Property(e => e.Fallo).HasColumnName("fallo");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
        });

        modelBuilder.Entity<Publicacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("publicacion");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaPublicacion)
                .HasColumnType("timestamp")
                .HasColumnName("fecha_publicacion");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(255)
                .HasColumnName("mensaje");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Publicacions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("publicacion_ibfk_1");
        });

        modelBuilder.Entity<Rolusuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rolusuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.EquipoId, "fk_usuario_equipos");

            entity.HasIndex(e => e.Rol, "fk_usuario_roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .HasColumnName("contrasena");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.EquipoId).HasColumnName("equipo_id");
            entity.Property(e => e.Experiencia)
                .HasMaxLength(50)
                .HasColumnName("experiencia");
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .HasColumnName("genero");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Posicion)
                .HasMaxLength(50)
                .HasColumnName("posicion");
            entity.Property(e => e.Rol).HasColumnName("rol");

            entity.HasOne(d => d.Equipo).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EquipoId)
                .HasConstraintName("fk_usuario_equipos");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
