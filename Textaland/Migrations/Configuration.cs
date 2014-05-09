namespace Textaland.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Textaland.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Textaland.DataAccessLayer.AppDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Textaland.DataAccessLayer.AppDataContext context)
        {

            var seedData = new List<TranslationRequestUpvote>
            {
                
            };

           // seedData.ForEach(d => context.TranslationRequests.Add(d));
            //context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
