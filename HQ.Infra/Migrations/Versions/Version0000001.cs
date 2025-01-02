using FluentMigrator;

namespace HQ.Infra.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_USER, "Create table to save the user information")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");

        // Users Table
        CreateTable("Users")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable().Unique()
            .WithColumn("Password").AsString(500).NotNullable();

        // Roles Table
        CreateTable("Roles")
            .WithColumn("Name").AsString(255).NotNullable();

        // UserRoles Table (Associativa)
        Create.Table("UserRoles")
            .WithColumn("UserId").AsGuid().NotNullable()
            .ForeignKey("FK_UserRoles_Users", "Users", "Id")
            .WithColumn("RoleId").AsGuid().NotNullable()
            .ForeignKey("FK_UserRoles_Roles", "Roles", "Id");

        // Chave Primária Composta para UserRoles
        Create.PrimaryKey("PK_UserRoles").OnTable("UserRoles").Columns("UserId", "RoleId");

        // Categories Table
        CreateTable("Categories")
            .WithColumn("Name").AsString(255).NotNullable();

        // Posts Table
        CreateTable("Posts")
            .WithColumn("Title").AsString(255).NotNullable()
            .WithColumn("Content").AsString(int.MaxValue).NotNullable()
            .WithColumn("PublishedAt").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentUTCDateTime)
            .WithColumn("ImageUrl").AsString(255).NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable()
            .ForeignKey("FK_Posts_Users", "Users", "Id")
            .WithColumn("CategoryId").AsGuid().NotNullable()
            .ForeignKey("FK_Posts_Categories", "Categories", "Id");

        // Tags Table
        CreateTable("Tags")
            .WithColumn("Name").AsString(255).NotNullable();

        Create.Table("PostsTags")
            .WithColumn("PostId").AsGuid().NotNullable()
            .ForeignKey("FK_PostsTags_Posts", "Posts", "Id")
            .WithColumn("TagId").AsGuid().NotNullable()
            .ForeignKey("FK_PostsTags_Tags", "Tags", "Id");

        // Chave Primária Composta
        Create.PrimaryKey("PK_PostsTags").OnTable("PostsTags").Columns("PostId", "TagId");

        // Comments Table
        CreateTable("Comments")
            .WithColumn("Content").AsString(1000).NotNullable()
            .WithColumn("CommentedAt").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentUTCDateTime)
            .WithColumn("PostId").AsGuid().NotNullable()
            .ForeignKey("FK_Comments_Posts", "Posts", "Id")
            .WithColumn("UserId").AsGuid().NotNullable()
            .ForeignKey("FK_Comments_Users", "Users", "Id");
    }
}