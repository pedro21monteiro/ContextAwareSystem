﻿// <auto-generated />
using System;
using ContextServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Context_aware_System.Migrations
{
    [DbContext(typeof(ContextAwareDb))]
    partial class ContextAwareDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("Models.ContextModels.Component", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("Models.ContextModels.Coordinator", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Coordinators");
                });

            modelBuilder.Entity("Models.ContextModels.Device", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Models.ContextModels.LastVerificationRegist", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("ComponentsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CoordinatorsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DevicesVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LinesVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OperatorsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProductionPlansVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProductionsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProductsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReasonsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequestsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Schedule_worker_linesVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StopsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SupervisorsVerification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WorkersVerification")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("lastVerificationRegists");
                });

            modelBuilder.Entity("Models.ContextModels.Line", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CoordinatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Priority")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("Models.ContextModels.Operator", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Operators");
                });

            modelBuilder.Entity("Models.ContextModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Cycle")
                        .HasColumnType("time");

                    b.Property<string>("LabelReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Models.ContextModels.Production", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Production_PlanId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Production_PlanId");

                    b.ToTable("Productions");
                });

            modelBuilder.Entity("Models.ContextModels.Production_Plan", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Goal")
                        .HasColumnType("int");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdate")
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

            modelBuilder.Entity("Models.ContextModels.Reason", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Reasons");
                });

            modelBuilder.Entity("Models.ContextModels.Request", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("requests");
                });

            modelBuilder.Entity("Models.ContextModels.Schedule_Worker_Line", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdate")
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

            modelBuilder.Entity("Models.ContextModels.Stop", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdate")
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

            modelBuilder.Entity("Models.ContextModels.Supervisor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Supervisors");
                });

            modelBuilder.Entity("Models.ContextModels.Worker", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdFirebase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

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
                    b.HasOne("Models.ContextModels.Component", null)
                        .WithMany()
                        .HasForeignKey("ComponentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.ContextModels.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.ContextModels.Coordinator", b =>
                {
                    b.HasOne("Models.ContextModels.Worker", "Worker")
                        .WithMany("Coordinators")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Models.ContextModels.Device", b =>
                {
                    b.HasOne("Models.ContextModels.Line", "Line")
                        .WithMany("Devices")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Line");
                });

            modelBuilder.Entity("Models.ContextModels.Line", b =>
                {
                    b.HasOne("Models.ContextModels.Coordinator", "Coordinator")
                        .WithMany("Lines")
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coordinator");
                });

            modelBuilder.Entity("Models.ContextModels.Operator", b =>
                {
                    b.HasOne("Models.ContextModels.Worker", "Worker")
                        .WithMany("Operators")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Models.ContextModels.Production", b =>
                {
                    b.HasOne("Models.ContextModels.Production_Plan", "Prod_Plan")
                        .WithMany("Productions")
                        .HasForeignKey("Production_PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prod_Plan");
                });

            modelBuilder.Entity("Models.ContextModels.Production_Plan", b =>
                {
                    b.HasOne("Models.ContextModels.Line", "Line")
                        .WithMany("Production_Plans")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.ContextModels.Product", "Product")
                        .WithMany("Production_Plans")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Line");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Models.ContextModels.Schedule_Worker_Line", b =>
                {
                    b.HasOne("Models.ContextModels.Line", "Line")
                        .WithMany("Schedules")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.ContextModels.Operator", "Operator")
                        .WithMany("Schedules")
                        .HasForeignKey("OperatorId");

                    b.HasOne("Models.ContextModels.Supervisor", "Supervisor")
                        .WithMany("Schedules")
                        .HasForeignKey("SupervisorId");

                    b.Navigation("Line");

                    b.Navigation("Operator");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("Models.ContextModels.Stop", b =>
                {
                    b.HasOne("Models.ContextModels.Line", "Line")
                        .WithMany("Stops")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.ContextModels.Reason", "Reason")
                        .WithMany("Stops")
                        .HasForeignKey("ReasonId");

                    b.Navigation("Line");

                    b.Navigation("Reason");
                });

            modelBuilder.Entity("Models.ContextModels.Supervisor", b =>
                {
                    b.HasOne("Models.ContextModels.Worker", "Worker")
                        .WithMany("Supervisors")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Models.ContextModels.Coordinator", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("Models.ContextModels.Line", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Production_Plans");

                    b.Navigation("Schedules");

                    b.Navigation("Stops");
                });

            modelBuilder.Entity("Models.ContextModels.Operator", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("Models.ContextModels.Product", b =>
                {
                    b.Navigation("Production_Plans");
                });

            modelBuilder.Entity("Models.ContextModels.Production_Plan", b =>
                {
                    b.Navigation("Productions");
                });

            modelBuilder.Entity("Models.ContextModels.Reason", b =>
                {
                    b.Navigation("Stops");
                });

            modelBuilder.Entity("Models.ContextModels.Supervisor", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("Models.ContextModels.Worker", b =>
                {
                    b.Navigation("Coordinators");

                    b.Navigation("Operators");

                    b.Navigation("Supervisors");
                });
#pragma warning restore 612, 618
        }
    }
}
