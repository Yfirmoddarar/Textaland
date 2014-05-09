using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
    public class AppDataContext : DbContext
    {
        public DbSet<SubtitleLine> SubtitleLines { get; set; }

    }
}