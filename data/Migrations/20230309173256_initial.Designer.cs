﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using data;

#nullable disable

namespace data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230309173256_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("entity.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"));

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.Property<int?>("TimezoneId")
                        .HasColumnType("int");

                    b.HasKey("CityId");

                    b.HasIndex("Slug");

                    b.HasIndex("StateId");

                    b.HasIndex("TimezoneId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("entity.Entities.CityName", b =>
                {
                    b.Property<int>("CityNameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityNameId"));

                    b.Property<int?>("CityId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("LanguageId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityNameId");

                    b.HasIndex("CityId");

                    b.HasIndex("LanguageId", "CityId")
                        .IsUnique();

                    b.ToTable("CityName");
                });

            modelBuilder.Entity("entity.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CountryId");

                    b.HasIndex("Slug");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("entity.Entities.CountryName", b =>
                {
                    b.Property<int>("CountryNameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryNameId"));

                    b.Property<int?>("CountryId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("LanguageId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryNameId");

                    b.HasIndex("CountryId");

                    b.HasIndex("LanguageId", "CountryId")
                        .IsUnique();

                    b.ToTable("CountryName");
                });

            modelBuilder.Entity("entity.Entities.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LanguageId"));

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId");

                    b.HasIndex("LanguageCode");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("entity.Entities.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateId"));

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StateId");

                    b.HasIndex("CountryId");

                    b.HasIndex("Slug");

                    b.ToTable("State");
                });

            modelBuilder.Entity("entity.Entities.StateName", b =>
                {
                    b.Property<int>("StateNameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateNameId"));

                    b.Property<int?>("LanguageId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StateId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("StateNameId");

                    b.HasIndex("StateId");

                    b.HasIndex("LanguageId", "StateId")
                        .IsUnique();

                    b.ToTable("StateName");
                });

            modelBuilder.Entity("entity.Entities.Timezone", b =>
                {
                    b.Property<int>("TimezoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TimezoneId"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GmtOffset")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("GmtOffsetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TzName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TimezoneId");

                    b.ToTable("Timezone");
                });

            modelBuilder.Entity("entity.Entities.City", b =>
                {
                    b.HasOne("entity.Entities.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId");

                    b.HasOne("entity.Entities.Timezone", "Timezone")
                        .WithMany()
                        .HasForeignKey("TimezoneId");

                    b.Navigation("State");

                    b.Navigation("Timezone");
                });

            modelBuilder.Entity("entity.Entities.CityName", b =>
                {
                    b.HasOne("entity.Entities.City", "City")
                        .WithMany("CityNames")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("entity.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("entity.Entities.CountryName", b =>
                {
                    b.HasOne("entity.Entities.Country", "Country")
                        .WithMany("CountryNames")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("entity.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("entity.Entities.State", b =>
                {
                    b.HasOne("entity.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("entity.Entities.StateName", b =>
                {
                    b.HasOne("entity.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("entity.Entities.State", "State")
                        .WithMany("StateNames")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("State");
                });

            modelBuilder.Entity("entity.Entities.City", b =>
                {
                    b.Navigation("CityNames");
                });

            modelBuilder.Entity("entity.Entities.Country", b =>
                {
                    b.Navigation("CountryNames");

                    b.Navigation("States");
                });

            modelBuilder.Entity("entity.Entities.State", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("StateNames");
                });
#pragma warning restore 612, 618
        }
    }
}
