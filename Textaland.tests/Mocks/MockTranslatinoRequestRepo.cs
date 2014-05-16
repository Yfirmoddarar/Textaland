using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaland.Interface;
using Textaland.Models;

namespace Textaland.tests.Mocks {
    public class MockTranslatinoRequestRepo : ITranslationRequestRepo {

        private readonly List<TranslationRequest> _trr;

        public MockTranslatinoRequestRepo(List<TranslationRequest> trr) {
            _trr = trr;
        }

        public IEnumerable<TranslationRequest> GetAllTranslationRequests() {
            var allTranslationRequests = from r in _trr
                                         orderby r._numUpvotes descending
                                         select r;
            return allTranslationRequests;
        }

        public IEnumerable<TranslationRequest> GetTranslationRequestById(int id) {
            var translationRequestById = from r in _trr
                                         where r.Id == id
                                         select r;
            return translationRequestById;
        }

        public void AddTranslationRequest(TranslationRequest newTranslationRequest) {
            int newId = 1;

            //If the list is not empty the new translation request will get id according
            //to the highest excisting Id.
            if (GetAllTranslationRequests().Count() > 0) {
                newId = _trr.Max(x => x.Id) + 1;
            }
            newTranslationRequest.Id = newId;
            _trr.Add(newTranslationRequest);
        }

        public void upVote(int id) {
            var req = _trr.First(r => r.Id == id);

            req._numUpvotes++;
        }

        public void RemoveTranslationRequestById(int id) {
            foreach (var item in _trr) {
                if (item.Id == id) {
                    _trr.Remove(item);
                }
            }
        }
    }
}
