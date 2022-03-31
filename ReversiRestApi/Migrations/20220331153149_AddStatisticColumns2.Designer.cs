﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReversiRestApi.DatabaseContexts;

namespace ReversiRestApi.Migrations
{
    [DbContext(typeof(ReversiDbContext))]
    [Migration("20220331153149_AddStatisticColumns2")]
    partial class AddStatisticColumns2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReversiRestApi.Game", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Board")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DidFinish")
                        .HasColumnType("bit");

                    b.Property<string>("Player1Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Player2Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerTurn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WinningPlayer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WinningPlayerColor")
                        .HasColumnType("int");

                    b.HasKey("Token");

                    b.ToTable("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
