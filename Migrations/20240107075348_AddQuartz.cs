using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

#nullable disable

namespace QuartzJob_Scheduler.Migrations
{
    public partial class AddQuartz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var scriptFolder = Directory.GetParent(assemblyDirectory).Parent.Parent.FullName;
            var alterDataSql = Path.Combine(scriptFolder, "Scripts/20240107075348_AddQuartz.sql");
            migrationBuilder.Sql(File.ReadAllText(alterDataSql));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Non-using method.
        }
    }
}
