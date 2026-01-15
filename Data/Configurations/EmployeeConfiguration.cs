using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Promtech_Machine_Test.Models;

namespace EmployeeManagement.Api.Data.Configurations
{

    // It separates database configuration from the model class
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {

        // This method is automatically called by EF Core
        // Used to configure table name, columns, keys, constraints
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Maps the Employee entity to the "Employees" table in database
            builder.ToTable("Employees");

            //makes the employeeid as the primary key
            builder.HasKey(e => e.EmployeeId);


            //values are auto generated when the new records are created.
            builder.Property(e => e.EmployeeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("EmployeeId");
            //Configure the name 
            builder.Property(e => e.Name)
                .IsRequired()
                //this feild is required and limited upto 100 characters.
                .HasMaxLength(100)
                .HasColumnName("Name");


            //Email = email is also required feild and the lenth is upto 100 charatcers
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Email");

            
            //property is also required and the limited character is of 50.
            builder.Property(e => e.Department)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Department");


            //Salary decimal with 10 digits and 2 decimal places.
            builder.Property(e => e.Salary)
                .HasPrecision(10, 2)
                .HasColumnName("Salary");

            //Configures created on Column
            builder.Property(e => e.CreatedOn)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnName("CreatedOn");

            // Unique constraint on Email
            builder.HasIndex(e => e.Email).IsUnique();
        }
    }
}