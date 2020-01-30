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

        public IpaDbContext() : base(CreateConnection(DatabaseConnectionString), false)
        {
            var initializer = new MigrateDatabaseToLatestVersion<IpaDbContext, Configuration>(true);
            initializer.InitializeDatabase(this);

            if (!Database.Exists())
            {
                // Надо потестировать эти инициализаторы и ручной запуск миграций

                Database.SetInitializer(new DropCreateDatabaseAlways<IpaDbContext>());

                Configuration config = new Configuration();
                DbMigrator migrator = new DbMigrator(config);

                foreach (string s in migrator.GetPendingMigrations())
                {
                    migrator.Update(s);
                }
            }

            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<Flow> Flows { get; set; }

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