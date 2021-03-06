// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sat.Recruitment.Api.Data;

namespace Sat.Recruitment.Api.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20210510124501_InitializeUserBase")]
    partial class InitializeUserBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sat.Recruitment.Api.Model.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoleType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasDiscriminator<int>("RoleType");
                });

            modelBuilder.Entity("Sat.Recruitment.Api.Model.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Email");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Sat.Recruitment.Api.Model.Normal", b =>
                {
                    b.HasBaseType("Sat.Recruitment.Api.Model.Role");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Sat.Recruitment.Api.Model.Premium", b =>
                {
                    b.HasBaseType("Sat.Recruitment.Api.Model.Role");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Sat.Recruitment.Api.Model.SuperUser", b =>
                {
                    b.HasBaseType("Sat.Recruitment.Api.Model.Role");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Sat.Recruitment.Api.Model.User", b =>
                {
                    b.HasOne("Sat.Recruitment.Api.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
