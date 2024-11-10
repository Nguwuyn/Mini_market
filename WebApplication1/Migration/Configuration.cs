namespace WebApplication1.Migrations
{
    using WebApplication1.Models;
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class DBConfiguration : DbMigrationsConfiguration<WebApplication1.Models.Model1>
    {
        public DBConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebApplication1.Models.Model1";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WebApplication1.Models.Model1 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Orders.ForEach(p =>
            {
                p.OrderStatus = "Đang giao hàng";
            });
        }
    }
}
