﻿// <auto-generated />
using System;
using DM.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DM.DAL.Migrations
{
    [DbContext(typeof(DmDbContext))]
    [Migration("20220705143425_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DM.DAL.Entities.FieldsEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("AssigneeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<long?>("IssuerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long?>("RecordEntityId")
                        .HasColumnType("bigint");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("IssuerId");

                    b.HasIndex("RecordEntityId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("DM.DAL.Entities.ObjectiveEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long?>("ProjectEntityId")
                        .HasColumnType("bigint");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("UserEntityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectEntityId");

                    b.HasIndex("UserEntityId");

                    b.ToTable("Objective");
                });

            modelBuilder.Entity("DM.DAL.Entities.ProjectEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("DM.DAL.Entities.RecordEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Record");
                });

            modelBuilder.Entity("DM.DAL.Entities.RoleEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("RoleEntity");
                });

            modelBuilder.Entity("DM.DAL.Entities.UserProjectEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<long?>("ProjectId1")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<long?>("UserId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId1");

                    b.HasIndex("UserId1");

                    b.ToTable("UserProject");
                });

            modelBuilder.Entity("DM.DAL.Entities.UserRoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoleEntity");
                });

            modelBuilder.Entity("DM.Entities.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DM.DAL.Entities.FieldsEntity", b =>
                {
                    b.HasOne("DM.Entities.UserEntity", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("DM.Entities.UserEntity", "Issuer")
                        .WithMany()
                        .HasForeignKey("IssuerId");

                    b.HasOne("DM.DAL.Entities.RecordEntity", null)
                        .WithMany("Fields")
                        .HasForeignKey("RecordEntityId");

                    b.Navigation("Assignee");

                    b.Navigation("Issuer");
                });

            modelBuilder.Entity("DM.DAL.Entities.ObjectiveEntity", b =>
                {
                    b.HasOne("DM.DAL.Entities.ProjectEntity", null)
                        .WithMany("Objectives")
                        .HasForeignKey("ProjectEntityId");

                    b.HasOne("DM.Entities.UserEntity", null)
                        .WithMany("Objectives")
                        .HasForeignKey("UserEntityId");
                });

            modelBuilder.Entity("DM.DAL.Entities.UserProjectEntity", b =>
                {
                    b.HasOne("DM.DAL.Entities.ProjectEntity", "Project")
                        .WithMany("Users")
                        .HasForeignKey("ProjectId1");

                    b.HasOne("DM.Entities.UserEntity", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId1");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DM.DAL.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("DM.DAL.Entities.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DM.Entities.UserEntity", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DM.DAL.Entities.ProjectEntity", b =>
                {
                    b.Navigation("Objectives");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("DM.DAL.Entities.RecordEntity", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("DM.DAL.Entities.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DM.Entities.UserEntity", b =>
                {
                    b.Navigation("Objectives");

                    b.Navigation("Projects");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
