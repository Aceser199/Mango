﻿// <auto-generated />
using Mango.Services.ProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mango.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Appetizer",
                            Description = "Punjabi Samosa",
                            ImageUrl = "",
                            Name = "Samosa",
                            Price = 15.0
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Appetizer",
                            Description = "Paneer Tikka with Marination",
                            ImageUrl = "",
                            Name = "Paneer Tikka",
                            Price = 13.0
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Entree",
                            Description = "Paneer Butter Masala with Creamy Gravy",
                            ImageUrl = "",
                            Name = "Paneer Butter Masala",
                            Price = 22.0
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Entree",
                            Description = "Mix Veg Curry with Spicy Gravy",
                            ImageUrl = "",
                            Name = "Mix Veg Curry",
                            Price = 18.0
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Appetizer",
                            Description = "Punjabi Samosa",
                            ImageUrl = "",
                            Name = "Samosa",
                            Price = 15.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
