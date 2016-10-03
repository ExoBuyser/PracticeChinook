using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Data.POCOs
{
    public class AlbumTrackSummary
    {
        public string Title { get; set; }
        public int TotalTracksforAlbum { get; set; }
        public decimal TotalPriceForalbumtracks { get; set; }
        public decimal AverageTrackLengthA { get; set; }
        public decimal AverageTrackLengthB { get; set; }
    }
}
