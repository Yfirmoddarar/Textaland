using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textaland.Models;

namespace Textaland.Interface {
    public interface ISubtitleFileRepo {
        IEnumerable<SubtitleFile> GetAllSubtitles();
        SubtitleFile GetSubtitleFileById(int id);
        void AddSubtitle(SubtitleFile _newSubtitleFile);
        void ChangeRating(int id, double rating);
        void RemoveSubtitle(int removeId);
        void wasDownloaded(int id);
        void setTime(int id);
        void setInTranslation(bool t, int id, string userId);
        void setDownload(bool t, int id);
        void setLanguage(string lan, int id);
        void setCounters(int id);
    }
}
