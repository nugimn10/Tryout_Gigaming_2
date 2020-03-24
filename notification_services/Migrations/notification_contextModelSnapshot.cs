﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using notification_services.Presistences;

namespace notification_services.Migrations
{
    [DbContext(typeof(notification_context))]
    partial class notification_contextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("notification_services.Domain.Entities.NotificationTb", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("message")
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .HasColumnType("text");

                    b.Property<DateTime>("update_at")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("notification_services.Domain.Entities.Notification_logsTb", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("email_destination")
                        .HasColumnType("text");

                    b.Property<int>("from")
                        .HasColumnType("integer");

                    b.Property<int>("notification_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("read_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("target")
                        .HasColumnType("integer");

                    b.Property<string>("type")
                        .HasColumnType("text");

                    b.Property<DateTime>("update_at")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.HasIndex("notification_id");

                    b.ToTable("Notification_Logs");
                });

            modelBuilder.Entity("notification_services.Domain.Entities.UserTb", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("address")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("notification_services.Domain.Entities.Notification_logsTb", b =>
                {
                    b.HasOne("notification_services.Domain.Entities.NotificationTb", "notification")
                        .WithMany()
                        .HasForeignKey("notification_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}