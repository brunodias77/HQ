using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace HQ.Infra.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
    {
        return Create.Table(table)
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid) // Usando Guid
            .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentUTCDateTime)
            .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true);
    }

    public override void Up()
    {
        throw new NotImplementedException();
    }
}