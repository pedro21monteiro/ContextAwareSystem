﻿// <auto-generated />
using System;
using ContextBuider.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContextBuider.Migrations
{
    [DbContext(typeof(ContextAwareDb))]
    [Migration("20230307115511_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ComponentProduct", b =>
                {
                    b.Property<int>("ComponentsId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("ComponentsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ComponentProduct");
                });

            modelBuilder.Entity("ContextBuider.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("ContextBuider.Models.Coordinator", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Coordinators");
                });

            modelBuilder.Entity("ContextBuider.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ContextBuider.Models.Line", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CoordinatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Priority")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("ContextBuider.Models.Operator", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Operators");
                });

            modelBuilder.Entity("ContextBuider.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Cycle")
                        .HasColumnType("time");

                    b.Property<string>("LabelReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ContextBuider.Models.Production", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<int>("Production_PlanId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Production_PlanId");

                    b.ToTable("Productions");
                });

            modelBuilder.Entity("ContextBuider.Models.Production_Plan", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Goal")
                        .HasColumnType("int");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Shift")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.HasIndex("ProductId");

                    b.ToTable("Production_Plans");
                });

            modelBuilder.Entity("ContextBuider.Models.Reason", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reasons");
                });

            modelBuilder.Entity("ContextBuider.Models.Schedule_Worker_Line", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<int?>("OperatorId")
                        .HasColumnType("int");

                    b.Property<int>("Shift")
                        .HasColumnType("int");

                    b.Property<int?>("SupervisorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.HasIndex("OperatorId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Schedule_Worker_Lines");
                });

            modelBuilder.Entity("ContextBuider.Models.Stop", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<bool>("Planned")
                        .HasColumnType("bit");

                    b.Property<int?>("ReasonId")
                        .HasColumnType("int");

                    b.Property<int>("Shift")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.HasIndex("ReasonId");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("ContextBuider.Models.Supervisor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Supervisors");
                });

            modelBuilder.Entity("ContextBuider.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdFirebase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("ComponentProduct", b =>
                {
                    b.HasOne("ContextBuider.Models.Component", null)
                        .WithMany()
                        .HasForeignKey("ComponentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContextBuider.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContextBuider.Models.Coordinator", b =>
                {
                    b.HasOne("ContextBuider.Models.Worker", "Worker")
                        .WithMany("Coordinators")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("ContextBuider.Models.Device", b =>
                {
                    b.HasOne("ContextBuider.Models.Line", "Line")
                        .WithMany("Devices")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Line");
                });

            modelBuilder.Entity("ContextBuider.Models.Line", b =>
                {
                    b.HasOne("ContextBuider.Models.Coordinator", "Coordinator")
                        .WithMany("Lines")
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coordinator");
                });

            modelBuilder.Entity("ContextBuider.Models.Operator", b =>
                {
                    b.HasOne("ContextBuider.Models.Worker", "Worker")
                        .WithMany("Operators")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("ContextBuider.Models.Production", b =>
                {
                    b.HasOne("ContextBuider.Models.Production_Plan", "Prod_Plan")
                        .WithMany("Productions")
                        .HasForeignKey("Production_PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prod_Plan");
                });

            modelBuilder.Entity("ContextBuider.Models.Production_Plan", b =>
                {
                    b.HasOne("ContextBuider.Models.Line", "Line")
                        .WithMany("Production_Plans")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContextBuider.Models.Product", "Product")
                        .WithMany("Production_Plans")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Line");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ContextBuider.Models.Schedule_Worker_Line", b =>
                {
                    b.HasOne("ContextBuider.Models.Line", "Line")
                        .WithMany("Schedules")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContextBuider.Models.Operator", "Operator")
                        .WithMany("Schedules")
                        .HasForeignKey("OperatorId");

                    b.HasOne("ContextBuider.Models.Supervisor", "Supervisor")
                        .WithMany("Schedules")
                        .HasForeignKey("SupervisorId");

                    b.Navigation("Line");

                    b.Navigation("Operator");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("ContextBuider.Models.Stop", b =>
                {
                    b.HasOne("ContextBuider.Models.Line", "Line")
                        .WithMany("Stops")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContextBuider.Models.Reason", "Reason")
                        .WithMany("Stops")
                        .HasForeignKey("ReasonId");

                    b.Navigation("Line");

                    b.Navigation("Reason");
                });

            modelBuilder.Entity("ContextBuider.Models.Supervisor", b =>
                {
                    b.HasOne("ContextBuider.Models.Worker", "Worker")
                        .WithMany("Supervisors")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("ContextBuider.Models.Coordinator", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("ContextBuider.Models.Line", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Production_Plans");

                    b.Navigation("Schedules");

                    b.Navigation("Stops");
                });

            modelBuilder.Entity("ContextBuider.Models.Operator", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("ContextBuider.Models.Product", b =>
                {
                    b.Navigation("Production_Plans");
                });

            modelBuilder.Entity("ContextBuider.Models.Production_Plan", b =>
                {
                    b.Navigation("Productions");
                });

            modelBuilder.Entity("ContextBuider.Models.Reason", b =>
                {
                    b.Navigation("Stops");
                });

            modelBuilder.Entity("ContextBuider.Models.Supervisor", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("ContextBuider.Models.Worker", b =>
                {
                    b.Navigation("Coordinators");

                    b.Navigation("Operators");

                    b.Navigation("Supervisors");
                });
#pragma warning restore 612, 618
        }
    }
}
