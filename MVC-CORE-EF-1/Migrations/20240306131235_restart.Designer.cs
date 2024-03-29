﻿// <auto-generated />
using System;
using MVC_CORE_EF_1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVC_CORE_EF_1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240306131235_restart")]
    partial class restart
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MVC_CORE_EF_1.Models.Shipping", b =>
                {
                    b.Property<int>("IdSpedizione")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSpedizione"));

                    b.Property<double>("CostoSpedizione")
                        .HasColumnType("float");

                    b.Property<DateTime>("DataConsegna")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSpedizione")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("IndirizzoDestinatario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NominativoDestinatario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PesoSpedizione")
                        .HasColumnType("float");

                    b.HasKey("IdSpedizione");

                    b.ToTable("Shippings");
                });

            modelBuilder.Entity("MVC_CORE_EF_1.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("CodiceFiscale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nominativo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartitaIva")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdUser");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
