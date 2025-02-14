﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizAPI.Data;

#nullable disable

namespace QuizAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250212020449_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuizAPI.Model.Bootcamper", b =>
                {
                    b.Property<int>("BootcamperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BootcamperId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BootcamperId");

                    b.ToTable("Bootcampers");
                });

            modelBuilder.Entity("QuizAPI.Model.BootcamperQuiz", b =>
                {
                    b.Property<int>("BootcamperId")
                        .HasColumnType("int");

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("BootcamperId", "QuizId");

                    b.HasIndex("QuizId");

                    b.ToTable("BootcamperQuizzes");
                });

            modelBuilder.Entity("QuizAPI.Model.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("Choices")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MQuestion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuizId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuizId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("QuizAPI.Model.Quiz", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuizId"));

                    b.Property<string>("QuizTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalScore")
                        .HasColumnType("int");

                    b.HasKey("QuizId");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("QuizAPI.Model.BootcamperQuiz", b =>
                {
                    b.HasOne("QuizAPI.Model.Bootcamper", "Bootcampers")
                        .WithMany("BootcamperQuizzes")
                        .HasForeignKey("BootcamperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuizAPI.Model.Quiz", "Quizzes")
                        .WithMany("BootcamperQuizzes")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bootcampers");

                    b.Navigation("Quizzes");
                });

            modelBuilder.Entity("QuizAPI.Model.Question", b =>
                {
                    b.HasOne("QuizAPI.Model.Quiz", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuizId");
                });

            modelBuilder.Entity("QuizAPI.Model.Bootcamper", b =>
                {
                    b.Navigation("BootcamperQuizzes");
                });

            modelBuilder.Entity("QuizAPI.Model.Quiz", b =>
                {
                    b.Navigation("BootcamperQuizzes");

                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
