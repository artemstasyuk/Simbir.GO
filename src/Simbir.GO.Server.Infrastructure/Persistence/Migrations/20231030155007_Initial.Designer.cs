﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Simbir.GO.Server.Infrastructure.Persistence;

#nullable disable

namespace Simbir.GO.Server.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231030155007_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Simbir.GO.Server.Domain.Accounts.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Balance")
                        .HasColumnType("double precision")
                        .HasColumnName("balance");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdDateTime");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("passwordHash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("passwordSalt");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updatedDateTime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pK_accounts");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("Simbir.GO.Server.Domain.Rents.Rent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint")
                        .HasColumnName("accountId");

                    b.Property<double?>("FinalPrice")
                        .IsRequired()
                        .HasColumnType("double precision")
                        .HasColumnName("finalPrice");

                    b.Property<double>("PriceOfUnit")
                        .HasColumnType("double precision")
                        .HasColumnName("priceOfUnit");

                    b.Property<string>("RentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("rentType");

                    b.Property<DateTime?>("TimeEnd")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timeEnd");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timeStart");

                    b.Property<long>("TransportId")
                        .HasColumnType("bigint")
                        .HasColumnName("transportId");

                    b.HasKey("Id")
                        .HasName("pK_rents");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("iX_rents_accountId");

                    b.HasIndex("TransportId")
                        .HasDatabaseName("iX_rents_transportId");

                    b.ToTable("rents", (string)null);
                });

            modelBuilder.Entity("Simbir.GO.Server.Domain.Transports.Transport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("CanBeRented")
                        .HasColumnType("boolean")
                        .HasColumnName("canBeRented");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<double?>("DayPrice")
                        .IsRequired()
                        .HasColumnType("double precision")
                        .HasColumnName("dayPrice");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("identifier");

                    b.Property<double?>("MinutePrice")
                        .IsRequired()
                        .HasColumnType("double precision")
                        .HasColumnName("minutePrice");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("model");

                    b.Property<long>("TransportOwnerId")
                        .HasColumnType("bigint")
                        .HasColumnName("transportOwnerId");

                    b.Property<string>("TransportType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("transportType");

                    b.HasKey("Id")
                        .HasName("pK_transports");

                    b.HasIndex("TransportOwnerId")
                        .HasDatabaseName("iX_transports_transportOwnerId");

                    b.ToTable("transports", (string)null);
                });

            modelBuilder.Entity("Simbir.GO.Server.Domain.Rents.Rent", b =>
                {
                    b.HasOne("Simbir.GO.Server.Domain.Accounts.Account", "Account")
                        .WithMany("AccountRents")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_rents_accounts_AccountTempId");

                    b.HasOne("Simbir.GO.Server.Domain.Transports.Transport", "Transport")
                        .WithMany("TransportRents")
                        .HasForeignKey("TransportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_rents_transports_TransportTempId");

                    b.Navigation("Account");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("Simbir.GO.Server.Domain.Transports.Transport", b =>
                {
                    b.HasOne("Simbir.GO.Server.Domain.Accounts.Account", "Account")
                        .WithMany("AccountTransport")
                        .HasForeignKey("TransportOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_transports_accounts_AccountTempId1");

                    b.OwnsOne("Simbir.GO.Server.Domain.Transports.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<long>("TransportId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision")
                                .HasColumnName("location_Latitude");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision")
                                .HasColumnName("location_Longitude");

                            b1.HasKey("TransportId");

                            b1.ToTable("transports");

                            b1.WithOwner()
                                .HasForeignKey("TransportId")
                                .HasConstraintName("fK_transports_transports_id");
                        });

                    b.Navigation("Account");

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("Simbir.GO.Server.Domain.Accounts.Account", b =>
                {
                    b.Navigation("AccountRents");

                    b.Navigation("AccountTransport");
                });

            modelBuilder.Entity("Simbir.GO.Server.Domain.Transports.Transport", b =>
                {
                    b.Navigation("TransportRents");
                });
#pragma warning restore 612, 618
        }
    }
}
