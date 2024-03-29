﻿// <auto-generated />
using System;
using BTCPayServer.RockstarDev.Plugins.Payroll.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BTCPayServer.RockstarDev.Plugins.Payroll.Data.Migrations
{
    [DbContext(typeof(PayrollPluginDbContext))]
    [Migration("20240328224732_AddingPayrollTranasaction")]
    partial class AddingPayrollTranasaction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("BTCPayServer.RockstarDev.Plugins.Payroll")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BTCPayServer.RockstarDev.Plugins.Payroll.Data.Models.PayrollInvoice", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Destination")
                        .HasColumnType("text");

                    b.Property<string>("InvoiceFilename")
                        .HasColumnType("text");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("TxnId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PayrollInvoices", "BTCPayServer.RockstarDev.Plugins.Payroll");
                });

            modelBuilder.Entity("BTCPayServer.RockstarDev.Plugins.Payroll.Data.Models.PayrollTransaction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Balance")
                        .HasColumnType("text");

                    b.Property<decimal>("BtcAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BtcJpyRate")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BtcUsdRate")
                        .HasColumnType("numeric");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<string>("InvoiceId")
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<decimal>("MinerFee")
                        .HasColumnType("numeric");

                    b.Property<string>("Recipient")
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PayrollTransactions", "BTCPayServer.RockstarDev.Plugins.Payroll");
                });

            modelBuilder.Entity("BTCPayServer.RockstarDev.Plugins.Payroll.Data.Models.PayrollUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("StoreId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("PayrollUsers", "BTCPayServer.RockstarDev.Plugins.Payroll");
                });

            modelBuilder.Entity("BTCPayServer.RockstarDev.Plugins.Payroll.Data.Models.PayrollInvoice", b =>
                {
                    b.HasOne("BTCPayServer.RockstarDev.Plugins.Payroll.Data.Models.PayrollUser", "User")
                        .WithMany("PayrollInvoices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("BTCPayServer.RockstarDev.Plugins.Payroll.Data.Models.PayrollUser", b =>
                {
                    b.Navigation("PayrollInvoices");
                });
#pragma warning restore 612, 618
        }
    }
}
