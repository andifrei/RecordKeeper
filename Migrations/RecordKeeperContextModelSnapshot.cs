﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecordKeeper.Models;

namespace RecordKeeper.Migrations
{
    [DbContext(typeof(RecordKeeperContext))]
    partial class RecordKeeperContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("RecordKeeper.Models.RecordItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Album")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<DateTime?>("AsOf");

                    b.Property<string>("Condition");

                    b.Property<string>("Description");

                    b.Property<string>("Label");

                    b.Property<decimal?>("Price");

                    b.Property<string>("Store");

                    b.Property<string>("StoreLocation");

                    b.Property<string>("Type");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.ToTable("RecordItem");
                });
#pragma warning restore 612, 618
        }
    }
}
