﻿// <auto-generated />
using GameService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20241013153645_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChoiceChoice", b =>
                {
                    b.Property<int>("ChoiceId")
                        .HasColumnType("int");

                    b.Property<int>("WeakerChoicesId")
                        .HasColumnType("int");

                    b.HasKey("ChoiceId", "WeakerChoicesId");

                    b.HasIndex("WeakerChoicesId");

                    b.ToTable("ChoiceChoice", "game");
                });

            modelBuilder.Entity("GameService.Domain.Entities.Choice", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("Id");

                    b.ToTable("choices", "game");
                });

            modelBuilder.Entity("ChoiceChoice", b =>
                {
                    b.HasOne("GameService.Domain.Entities.Choice", null)
                        .WithMany()
                        .HasForeignKey("ChoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameService.Domain.Entities.Choice", null)
                        .WithMany()
                        .HasForeignKey("WeakerChoicesId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
