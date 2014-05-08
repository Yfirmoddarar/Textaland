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

            var subtitleLines = new List<SubtitleLine>
            {
                new SubtitleLine{_lineNumber=1,_line1="First line of text.",_line2="Second line of text.",_time="Timestring:)"},
                new SubtitleLine{_lineNumber=2,_line1="First  of text.",_line2="Second  of text.",_time="Timestring:)"},
                new SubtitleLine{_lineNumber=3,_line1="First line  text.",_line2="Second line  text.",_time="Timestring:)"},
                new SubtitleLine{_lineNumber=4,_line1="First  text.",_line2="Second text.",_time="Timestring:)"}
            };

            subtitleLines.ForEach(l => context.SubtitleLines.Add(l));
            context.SaveChanges();

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
