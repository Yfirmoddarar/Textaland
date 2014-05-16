using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaland.Models;

namespace Textaland.Interface {
    public interface ITranslationRequestRepo {
        IEnumerable<TranslationRequest> GetAllTranslationRequests();
        IEnumerable<TranslationRequest> GetTranslationRequestById(int id);
        void AddTranslationRequest(TranslationRequest newTranslationRequest);
        void upVote(int id);
        void RemoveTranslationRequestById(TranslationRequest tr);
    }
}
