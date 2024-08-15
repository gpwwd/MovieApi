﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieApiMvc.DataBaseAccess.Context;

#nullable disable

namespace MovieApiMvc.DataBaseAccess.Migrations
{
    [DbContext(typeof(MovieDataBaseContext))]
    [Migration("20240811003940_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("CountryEntityMovieEntity", b =>
                {
                    b.Property<Guid>("CountriesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MoviesId")
                        .HasColumnType("TEXT");

                    b.HasKey("CountriesId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CountryEntityMovieEntity");
                });

            modelBuilder.Entity("GenreEntityMovieEntity", b =>
                {
                    b.Property<Guid>("GenresId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MoviesId")
                        .HasColumnType("TEXT");

                    b.HasKey("GenresId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("GenreEntityMovieEntity");
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.BudgetEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("TEXT");

                    b.Property<double>("value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MovieId")
                        .IsUnique();

                    b.ToTable("Budgets", (string)null);
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.CountryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.GenreEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres", (string)null);
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.MovieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AlternativeName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSeries")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieLength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Top250")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Movies", (string)null);
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.RatingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("TEXT");

                    b.Property<double>("filmCritics")
                        .HasColumnType("REAL");

                    b.Property<double>("imdb")
                        .HasColumnType("REAL");

                    b.Property<double>("kp")
                        .HasColumnType("REAL");

                    b.Property<double>("russianFilmCritics")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MovieId")
                        .IsUnique();

                    b.ToTable("Ratings", (string)null);
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.UsersEntities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("MovieEntityUserEntity", b =>
                {
                    b.Property<Guid>("FavMovieUsersId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FavMoviesId")
                        .HasColumnType("TEXT");

                    b.HasKey("FavMovieUsersId", "FavMoviesId");

                    b.HasIndex("FavMoviesId");

                    b.ToTable("MovieEntityUserEntity");
                });

            modelBuilder.Entity("MovieEntityUserEntity1", b =>
                {
                    b.Property<Guid>("WatchLaterMoviesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("WatchLaterUsersId")
                        .HasColumnType("TEXT");

                    b.HasKey("WatchLaterMoviesId", "WatchLaterUsersId");

                    b.HasIndex("WatchLaterUsersId");

                    b.ToTable("MovieEntityUserEntity1");
                });

            modelBuilder.Entity("CountryEntityMovieEntity", b =>
                {
                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.CountryEntity", null)
                        .WithMany()
                        .HasForeignKey("CountriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.MovieEntity", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreEntityMovieEntity", b =>
                {
                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.GenreEntity", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.MovieEntity", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.BudgetEntity", b =>
                {
                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.MovieEntity", "Movie")
                        .WithOne("Budget")
                        .HasForeignKey("MovieApiMvc.DataBaseAccess.Entities.BudgetEntity", "MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.RatingEntity", b =>
                {
                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.MovieEntity", "Movie")
                        .WithOne("Rating")
                        .HasForeignKey("MovieApiMvc.DataBaseAccess.Entities.RatingEntity", "MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieEntityUserEntity", b =>
                {
                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.UsersEntities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("FavMovieUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.MovieEntity", null)
                        .WithMany()
                        .HasForeignKey("FavMoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieEntityUserEntity1", b =>
                {
                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.MovieEntity", null)
                        .WithMany()
                        .HasForeignKey("WatchLaterMoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieApiMvc.DataBaseAccess.Entities.UsersEntities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("WatchLaterUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieApiMvc.DataBaseAccess.Entities.MovieEntity", b =>
                {
                    b.Navigation("Budget");

                    b.Navigation("Rating");
                });
#pragma warning restore 612, 618
        }
    }
}
