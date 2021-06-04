﻿// <auto-generated />
using System;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BookStore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210604033503_add-datetime-record")]
    partial class adddatetimerecord
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BookStore.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CurrentAmount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookStore.Models.BookAmountChangingRecord", b =>
                {
                    b.Property<int>("ChangeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AmountChanged")
                        .HasColumnType("integer");

                    b.Property<int?>("BookId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateChanged")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsImport")
                        .HasColumnType("boolean");

                    b.HasKey("ChangeNumber");

                    b.HasIndex("BookId");

                    b.ToTable("BookAmountChangingRecord");
                });

            modelBuilder.Entity("BookStore.Models.BookType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BookTypes");
                });

            modelBuilder.Entity("BookStore.Models.Configuration", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Name");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("BookStore.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("CurrentDebt")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BookStore.Models.Book", b =>
                {
                    b.HasOne("BookStore.Models.BookType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("BookStore.Models.BookAmountChangingRecord", b =>
                {
                    b.HasOne("BookStore.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.Navigation("Book");
                });
#pragma warning restore 612, 618
        }
    }
}
