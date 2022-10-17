﻿// <auto-generated />
using System;
using AMO_4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AMO_4.Migrations
{
    [DbContext(typeof(MyWebApiContext))]
    partial class MyWebApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AMO_4.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("codeMessage")
                        .HasColumnType("integer");

                    b.Property<int>("criticity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("date_heure")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("dump")
                        .HasColumnType("text");

                    b.Property<int>("produitId")
                        .HasColumnType("integer");

                    b.Property<string>("type")
                        .HasColumnType("text");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("produitId");

                    b.HasIndex("userId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("AMO_4.Models.Produit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("ref_produit")
                        .HasColumnType("text");

                    b.Property<string>("version")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("AMO_4.Models.Role", b =>
                {
                    b.Property<int>("roleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("color")
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("roleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AMO_4.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<string>("ref_user")
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .HasColumnType("text");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("roleId")
                        .HasColumnType("integer");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("roleId", "userId");

                    b.HasIndex("userId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("AMO_4.Models.Log", b =>
                {
                    b.HasOne("AMO_4.Models.Produit", "produit")
                        .WithMany()
                        .HasForeignKey("produitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMO_4.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("produit");

                    b.Navigation("user");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("AMO_4.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMO_4.Models.User", null)
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
