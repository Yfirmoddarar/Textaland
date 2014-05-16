using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaland.Models;

namespace Textaland.Interface {
    public interface ISubtitleCommentRepo {
        IEnumerable<SubtitleComment> GetAllComments();
        IEnumerable<SubtitleComment> GetCommentsById(int id);
        SubtitleComment GetSingleCommentById(int id);
        void AddComment(SubtitleComment newSubtitleComment);
        void RemoveComment(SubtitleComment comment);
    }
}
