using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using ConsoleApp.Entities;
using ConsoleApp.Migrations;

namespace ConsoleApp.DataAccess
{
    public class IpaDbContext : DbContext
    {
        public static string DatabaseConnectionString = @"d:\work835\Temp\test.db";

        static IpaDbContext()
        {
            var initializer = new MigrateDatabaseToLatestVersion<IpaDbContext, Configuration>(true);
            Database.SetInitializer(initializer);
        }

        public IpaDbContext() : base(CreateConnection(DatabaseConnectionString), false)
        {
            // В случае SQLite пустая база все равно будет создана в методе CreateConnection
            if (!Database.Exists())
            {
                // Создание базы и ручной запуск миграций
                CreateNewDatabaseWithMigrations();
            }

            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<Flow> Flows { get; set; }

        private static void CreateNewDatabaseWithMigrations()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<IpaDbContext>());

            var config = new Configuration();
            var migrator = new DbMigrator(config);

            foreach (var migrationName in migrator.GetPendingMigrations())
            {
                migrator.Update(migrationName);
            }
        }

        private static SQLiteConnection CreateConnection(string path)
        {
            var builder = (SQLiteConnectionStringBuilder) SQLiteProviderFactory.Instance.CreateConnectionStringBuilder();
            if (builder == null) return null;

            builder.DataSource = path;
            builder.FailIfMissing = false;
            return new SQLiteConnection(builder.ToString());
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}