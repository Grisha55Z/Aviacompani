﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aviacompani.Models;

public partial class KiselevContext : DbContext
{
    public KiselevContext()
    {
    }

    public KiselevContext(DbContextOptions<KiselevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=192.168.2.101;User=root;Database=Kiselev;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Flight", "Kiselev");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Flight1)
                .HasMaxLength(100)
                .HasColumnName("Flight");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Planes", "Kiselev");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Planes).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
