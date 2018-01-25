﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using qr_reader.Models;
using System;

namespace qr_reader.Migrations
{
    [DbContext(typeof(DbEntity))]
    [Migration("20171113075807_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("qr_reader.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleNumber");

                    b.Property<int>("Article_typeId");

                    b.Property<string>("Lowlimit");

                    b.Property<string>("Uplimit");

                    b.HasKey("Id");

                    b.HasIndex("Article_typeId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("qr_reader.Models.Article_type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Article_types");
                });

            modelBuilder.Entity("qr_reader.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderNumber");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("qr_reader.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Results")
                        .HasMaxLength(700);

                    b.Property<int>("TestId");

                    b.Property<int?>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("UnitId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("qr_reader.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Article_typeId");

                    b.Property<DateTime>("Datetime");

                    b.Property<string>("Name");

                    b.Property<string>("Property")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("Article_typeId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("qr_reader.Models.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticleId");

                    b.Property<int>("OrderId");

                    b.Property<int>("SerialNumber");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("OrderId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("qr_reader.Models.Article", b =>
                {
                    b.HasOne("qr_reader.Models.Article_type", "Article_type")
                        .WithMany("Articles")
                        .HasForeignKey("Article_typeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qr_reader.Models.Result", b =>
                {
                    b.HasOne("qr_reader.Models.Test", "Test")
                        .WithMany("Results")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qr_reader.Models.Unit", "Unit")
                        .WithMany("Results")
                        .HasForeignKey("UnitId");
                });

            modelBuilder.Entity("qr_reader.Models.Test", b =>
                {
                    b.HasOne("qr_reader.Models.Article_type", "Article_type")
                        .WithMany("Tests")
                        .HasForeignKey("Article_typeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qr_reader.Models.Unit", b =>
                {
                    b.HasOne("qr_reader.Models.Article", "Article")
                        .WithMany("Units")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qr_reader.Models.Order", "Order")
                        .WithMany("Units")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
