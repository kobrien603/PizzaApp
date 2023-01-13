using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaApp.Server.DAL.Migrations
{
    public partial class createStandardDatabaseNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "users",
                newName: "profile_picture");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "users",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "users",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "users",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "users",
                newName: "deleted_date");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "users",
                newName: "date_of_birth");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "users",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "items",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "items",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "items",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "items",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "company",
                newName: "zip");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "company",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "company",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "company",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "company",
                newName: "address2");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "company",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "company",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "company",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "company",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "company",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "company",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "company",
                newName: "company_name");

            migrationBuilder.RenameColumn(
                name: "CompanyLogo",
                table: "company",
                newName: "company_logo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "categories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "categories",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "profile_picture",
                table: "users",
                newName: "ProfilePicture");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "users",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "users",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "deleted_date",
                table: "users",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "users",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "users",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "items",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "items",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "items",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "items",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "items",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "zip",
                table: "company",
                newName: "Zip");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "company",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "company",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "company",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address2",
                table: "company",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "company",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "company",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "company",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "company",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "company",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "company",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "company_name",
                table: "company",
                newName: "CompanyName");

            migrationBuilder.RenameColumn(
                name: "company_logo",
                table: "company",
                newName: "CompanyLogo");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "categories",
                newName: "ID");
        }
    }
}
