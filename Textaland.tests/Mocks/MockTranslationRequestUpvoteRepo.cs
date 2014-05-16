using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaland.Interface;
using Textaland.Models;

namespace Textaland.tests.Mocks {
    class MockTranslationRequestUpvoteRepo : ITranslationRequestUpvoteRepo {

        private readonly List<TranslationRequestUpvote> _trur;

        public MockTranslationRequestUpvoteRepo(List<TranslationRequestUpvote> trur) {
            _trur = trur;
        }

        public IEnumerable<TranslationRequestUpvote> GetAllUpvotes() {
            var allUpvotes = from u in _trur
                             select u;
            return allUpvotes;
        }

        public IEnumerable<TranslationRequestUpvote> GetUpvoteById(int id) {
            var correctId = from u in _trur
                            where u._requestId == id
                            select u;
            return correctId;
        }

        public void AddUpvote(TranslationRequestUpvote newUpvote) {
            int newId = 1;

            // But if the list is not empty than it will get id according the the list.
            if (GetAllUpvotes().Count() > 0) {
                newId = _trur.Max(x => x.Id) + 1;
            }

            // Give the new line the id.
            newUpvote.Id = newId;
            //And add the new line to the list.
            _trur.Add(newUpvote);
        }
    }
}
