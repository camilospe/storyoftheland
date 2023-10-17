﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoriesOfTheLand.Data;

#nullable disable

namespace StoriesOfTheLand.Migrations
{
    [DbContext(typeof(StoriesOfTheLandContext))]
    [Migration("20231017191347_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("StorisOfTheLand.Models.Specimen", b =>
                {
                    b.Property<int>("SpecimenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecimenDescription")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.HasKey("SpecimenID");

                    b.ToTable("Specimen");
                });
#pragma warning restore 612, 618
        }
    }
}
