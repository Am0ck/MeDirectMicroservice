﻿// <auto-generated />
using System;
using MeDirectMicroservice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeDirectMicroservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230407122114_TradeAndCurrency")]
    partial class TradeAndCurrency
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CurrencyExchangeTrade", b =>
                {
                    b.Property<string>("CurrenciesCurrencySymbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("ExchangeTradesId")
                        .HasColumnType("bigint");

                    b.HasKey("CurrenciesCurrencySymbol", "ExchangeTradesId");

                    b.HasIndex("ExchangeTradesId");

                    b.ToTable("CurrencyExchangeTrade");
                });

            modelBuilder.Entity("MeDirectMicroservice.Models.Currency", b =>
                {
                    b.Property<string>("CurrencySymbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CurrencySymbol");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("MeDirectMicroservice.Models.ExchangeTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("ExchangeTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ExchangeTrades");
                });

            modelBuilder.Entity("CurrencyExchangeTrade", b =>
                {
                    b.HasOne("MeDirectMicroservice.Models.Currency", null)
                        .WithMany()
                        .HasForeignKey("CurrenciesCurrencySymbol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeDirectMicroservice.Models.ExchangeTrade", null)
                        .WithMany()
                        .HasForeignKey("ExchangeTradesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}