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
			
        //    var seedData = new List<SubtitleLine> {
        //        new SubtitleLine{ Id = 1,
        //            _line1 = "One",
        //            _line2 = "TWO",
        //            _line3 = "",
        //            _lineNumber = 1,
        //            _textFileId = 1,
        //            _time="timestring"},
        //        new SubtitleLine{ Id = 2,
        //            _line1 = "One",
        //            _line2 = "TWO",
        //            _line3 = "",
        //            _lineNumber = 2,
        //            _textFileId = 1,
        //            _time="timestring"},
        //        new SubtitleLine{ Id = 3,
        //            _line1 = "One",
        //            _line2 = "TWO",
        //            _line3 = "",
        //            _lineNumber = 3,
        //            _textFileId = 1,
        //            _time="timestring"}
        //    };
			
        //    seedData.ForEach(d => context.SubtitleLines.Add(d));
        //    context.SaveChanges();
			

			/*
			var seedData = new List<SubtitleComment> {
				new SubtitleComment{ _text="Herna er comment sem er ekki donalegt", _textFileId=2 }
			};

			seedData.ForEach(d => context.SubtitleComments.Add(d));
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
