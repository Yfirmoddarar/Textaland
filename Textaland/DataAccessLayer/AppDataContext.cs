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
		public DbSet<SubtitleFile> SubtitleFiles { get; set; }
		public DbSet<SubtitleComment> SubtitleComments { get; set; }
		public DbSet<TranslationRequest> TranslationRequests { get; set; }
		public DbSet<TranslationRequestUpvote> TranslationRequestUpvotes { get; set; }
    }
}