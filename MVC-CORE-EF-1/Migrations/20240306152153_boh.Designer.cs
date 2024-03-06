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
    [Migration("20240306152153_boh")]
    partial class boh
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

                    b.HasIndex("IdUser");

                    b.ToTable("Shippings");
                });

            modelBuilder.Entity("MVC_CORE_EF_1.Models.ShippingDetail", b =>
                {
                    b.Property<int>("IdShippingDetail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdShippingDetail"));

                    b.Property<DateTime>("DataAggiornamento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdSpedizione")
                        .HasColumnType("int");

                    b.Property<string>("LuogoCorrente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoteSpedizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatoSpedizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdShippingDetail");

                    b.HasIndex("IdSpedizione");

                    b.ToTable("ShippingDetails");
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

            modelBuilder.Entity("MVC_CORE_EF_1.Models.Shipping", b =>
                {
                    b.HasOne("MVC_CORE_EF_1.Models.User", "User")
                        .WithMany("Shippings")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MVC_CORE_EF_1.Models.ShippingDetail", b =>
                {
                    b.HasOne("MVC_CORE_EF_1.Models.Shipping", "Shipping")
                        .WithMany("ShippingDetails")
                        .HasForeignKey("IdSpedizione")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("MVC_CORE_EF_1.Models.Shipping", b =>
                {
                    b.Navigation("ShippingDetails");
                });

            modelBuilder.Entity("MVC_CORE_EF_1.Models.User", b =>
                {
                    b.Navigation("Shippings");
                });
#pragma warning restore 612, 618
        }
    }
}
