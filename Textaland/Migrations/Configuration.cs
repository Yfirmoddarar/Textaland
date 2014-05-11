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
			
			/*var seedData = new List<SubtitleFile> {
				new SubtitleFile{ _name="Avatar", _description="wblaba blatt folk?"}
			};
			
			 

			seedData.ForEach(d => context.SubtitleFiles.Add(d));
			context.SaveChanges();
			 */
	

			/*
			var seedData = new List<TranslationRequestUpvote>
			{
				new TranslationRequestUpvote{_requestId=1},
				new TranslationRequestUpvote{_requestId=1}
			};
            
			seedData.ForEach(d => context.TranslationRequestUpvotes.Add(d));
			context.SaveChanges();

			*/

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
