// <auto-generated />
using System;
using MCA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace APIMCA.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220623041929_Update2")]
    partial class Update2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            modelBuilder.Entity("MCA.Models.Parameter_Version", b =>
                {
                    b.Property<int>("prv_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("prv_date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int?>("prv_headerid")
                        .HasColumnType("integer");

                    b.Property<string>("prv_module")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("prv_status")
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime?>("prv_sync_plan")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("prv_unique_parameter")
                        .HasColumnType("integer");

                    b.Property<string>("prv_version")
                        .HasColumnType("varchar(50)");

                    b.HasKey("prv_id");

                    b.ToTable("parameter_version", "version");
                });

            modelBuilder.Entity("MCA.Models.Rule", b =>
                {
                    b.Property<int>("rul_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("rul_applied")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_approved_by")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_approved_status")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_category")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_condition")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("rul_created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("rul_created_by")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_desc")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("rul_id_ori")
                        .HasColumnType("integer");

                    b.Property<bool>("rul_is_active")
                        .HasColumnType("boolean");

                    b.Property<bool?>("rul_is_deleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("rul_is_used")
                        .HasColumnType("boolean");

                    b.Property<string>("rul_modified")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_modified_by")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_name")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("rul_output")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("rul_output_type")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("rul_type")
                        .HasColumnType("integer");

                    b.Property<string>("rul_version")
                        .HasColumnType("varchar(20)");

                    b.HasKey("rul_id");

                    b.ToTable("rule", "public");
                });

            modelBuilder.Entity("MCA.Models.RuleFinal", b =>
                {
                    b.Property<int>("rul_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("rul_applied")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_approved_by")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_approved_status")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_category")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_condition")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("rul_created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("rul_created_by")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_desc")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_id_ori")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("rul_is_active")
                        .HasColumnType("boolean");

                    b.Property<bool?>("rul_is_deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValueSql("false");

                    b.Property<bool>("rul_is_used")
                        .HasColumnType("boolean");

                    b.Property<string>("rul_modified")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_modified_by")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("rul_name")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("rul_output")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("rul_output_type")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("rul_type")
                        .HasColumnType("integer");

                    b.Property<string>("rul_version")
                        .HasColumnType("varchar(20)");

                    b.HasKey("rul_id");

                    b.ToTable("rule_final", "public");
                });
#pragma warning restore 612, 618
        }
    }
}
