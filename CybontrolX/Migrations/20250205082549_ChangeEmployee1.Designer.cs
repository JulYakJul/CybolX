﻿// <auto-generated />
using System;
using CybontrolX.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CybontrolX.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250205082549_ChangeEmployee1")]
    partial class ChangeEmployee1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CybontrolX.DBModels.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Computer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ComputerIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("CurrentClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("SessionEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("SessionStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CurrentClientId");

                    b.ToTable("Computers");
                });

            modelBuilder.Entity("CybontrolX.DBModels.DutySchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DutyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("ShiftEnd")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("ShiftStart")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.ToTable("DutySchedules");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DutyScheduleId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan?>("ShiftEnd")
                        .HasColumnType("interval");

                    b.Property<TimeSpan?>("ShiftStart")
                        .HasColumnType("interval");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DutyScheduleId")
                        .IsUnique();

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ConfirmedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("FailureReason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NotificationUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PaymentDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RedirectUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PurchasePrice")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int?>("SalePrice")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("GeneratedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReportData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<int>("ComputerId")
                        .HasColumnType("integer");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReservationEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReservationStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TariffId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ComputerId");

                    b.HasIndex("TariffId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<int>("ComputerId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("SessionEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("SessionStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TariffId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ComputerId");

                    b.HasIndex("TariffId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Tariff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Days")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<TimeSpan>("SessionTime")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Computer", b =>
                {
                    b.HasOne("CybontrolX.DBModels.Client", "CurrentClient")
                        .WithMany()
                        .HasForeignKey("CurrentClientId");

                    b.Navigation("CurrentClient");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Employee", b =>
                {
                    b.HasOne("CybontrolX.DBModels.DutySchedule", "DutySchedule")
                        .WithOne("Employee")
                        .HasForeignKey("CybontrolX.DBModels.Employee", "DutyScheduleId");

                    b.Navigation("DutySchedule");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Payment", b =>
                {
                    b.HasOne("CybontrolX.DBModels.Client", "Client")
                        .WithMany("Payments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CybontrolX.DBModels.Employee", "Employee")
                        .WithMany("Payments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Reservation", b =>
                {
                    b.HasOne("CybontrolX.DBModels.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CybontrolX.DBModels.Computer", "Computer")
                        .WithMany("Reservations")
                        .HasForeignKey("ComputerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CybontrolX.DBModels.Tariff", "Tariff")
                        .WithMany("Reservations")
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Computer");

                    b.Navigation("Tariff");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Session", b =>
                {
                    b.HasOne("CybontrolX.DBModels.Client", "Client")
                        .WithMany("Sessions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CybontrolX.DBModels.Computer", "Computer")
                        .WithMany()
                        .HasForeignKey("ComputerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CybontrolX.DBModels.Tariff", "Tariff")
                        .WithMany("Sessions")
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Computer");

                    b.Navigation("Tariff");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Client", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Reservations");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Computer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("CybontrolX.DBModels.DutySchedule", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("CybontrolX.DBModels.Employee", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("CybontrolX.DBModels.Tariff", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
