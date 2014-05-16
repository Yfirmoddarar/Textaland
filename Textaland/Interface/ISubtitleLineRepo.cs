using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaland.Models;

namespace Textaland.Interface {
    public interface ISubtitleLineRepo {
        IEnumerable<SubtitleLine> GetAllLines();
        IEnumerable<SubtitleLine> GetLinesById(int id);
        void AddLine(SubtitleLine newSubtitleLine);
        void RemoveLines(int id);
        void copyLines(int id, int oldId);
        void UpdateLine(SubtitleLine newLine);
    }
}
