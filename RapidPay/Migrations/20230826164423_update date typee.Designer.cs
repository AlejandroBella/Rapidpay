﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RapidPay.Data;

#nullable disable

namespace RapidPay.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20230826164423_update date typee")]
    partial class updatedatetypee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("RapidPay.Data.Model.CardHolderModel", b =>
                {
                    b.Property<string>("IdNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("LastUpdate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdNumber");

                    b.ToTable("CardHolders");
                });

            modelBuilder.Entity("RapidPay.Data.Model.CardModel", b =>
                {
                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardHolderModelIdNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("HolderIdNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PIN")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Number");

                    b.HasIndex("CardHolderModelIdNumber");

                    b.ToTable("CreditCard");
                });

            modelBuilder.Entity("RapidPay.Data.Model.CardModel", b =>
                {
                    b.HasOne("RapidPay.Data.Model.CardHolderModel", null)
                        .WithMany("Cards")
                        .HasForeignKey("CardHolderModelIdNumber");
                });

            modelBuilder.Entity("RapidPay.Data.Model.CardHolderModel", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
