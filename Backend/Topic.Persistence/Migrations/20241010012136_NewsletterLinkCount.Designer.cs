﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Topic.Persistence.Contexts;

#nullable disable

namespace Topic.Persistence.Migrations
{
    [DbContext(typeof(TopicDbContext))]
    [Migration("20241010012136_NewsletterLinkCount")]
    partial class NewsletterLinkCount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Topic.Domain.Entities.Newsletter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string[]>("Keywords")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int>("LinksCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Newsletter");
                });

            modelBuilder.Entity("Topic.Domain.Entities.NewsletterLink", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("NewsletterId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.HasKey("Id");

                    b.HasIndex("NewsletterId");

                    b.ToTable("NewsletterLink");
                });

            modelBuilder.Entity("Topic.Domain.Entities.NewsletterLink", b =>
                {
                    b.HasOne("Topic.Domain.Entities.Newsletter", "Newsletter")
                        .WithMany("Links")
                        .HasForeignKey("NewsletterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Newsletter");
                });

            modelBuilder.Entity("Topic.Domain.Entities.Newsletter", b =>
                {
                    b.Navigation("Links");
                });
#pragma warning restore 612, 618
        }
    }
}
