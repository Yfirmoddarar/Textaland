using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaland.Models;

namespace Textaland.Interface {
    public interface ITranslationRequestUpvoteRepo {
        IEnumerable<TranslationRequestUpvote> GetAllUpvotes();
        IEnumerable<TranslationRequestUpvote> GetUpvoteById(int id);
        void AddUpvote(TranslationRequestUpvote newUpvote);
    }
}
