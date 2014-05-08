using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class SubtitleCommentRepo
	{
		private static SubtitleCommentRepo _instance;

		public static SubtitleCommentRepo Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SubtitleCommentRepo();
				return _instance;
			}
		}

		private List<SubtitleComment> _subtitleComments = null;

	}
}