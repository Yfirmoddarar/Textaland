using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaland.Models;

namespace Textaland.Interface {
    public interface IRatingRepo {
        IEnumerable<Rating> GetAllRatings();
        void AddRating(Rating newRating);
    }
}
