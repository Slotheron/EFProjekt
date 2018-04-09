﻿// <auto-generated />
using Game.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Game.Data.Migrations
{
    [DbContext(typeof(GameContext))]
    partial class GameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Game.Domain.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Game.Domain.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaxRounds");

                    b.Property<DateTime>("Time");

                    b.Property<int>("TournamentId");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Game.Domain.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Name");

                    b.Property<string>("Nationality");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Game.Domain.PlayerCharacter", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("CharacterId");

                    b.Property<int>("Color");

                    b.Property<string>("Position");

                    b.HasKey("PlayerId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("PlayerCharacter");
                });

            modelBuilder.Entity("Game.Domain.PlayerMatch", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("MatchId");

                    b.HasKey("PlayerId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("PlayerMatch");
                });

            modelBuilder.Entity("Game.Domain.SpecialMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacterId");

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("Game.Domain.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name");

                    b.Property<double>("PrizeMoney");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("Game.Domain.PlayerCharacter", b =>
                {
                    b.HasOne("Game.Domain.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Game.Domain.Player", "Player")
                        .WithMany("Characters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Game.Domain.PlayerMatch", b =>
                {
                    b.HasOne("Game.Domain.Match", "Match")
                        .WithMany("Players")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Game.Domain.Player", "Player")
                        .WithMany("Matches")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
