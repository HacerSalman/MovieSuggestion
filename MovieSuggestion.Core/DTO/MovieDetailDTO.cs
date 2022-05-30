using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.DTO
{
    public class MovieDetailDTO
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }

        public double Score { get; set; }

        public bool Adult { get; set; }

        public string BackdropPathUrl { get; set; }

        public long SourceId { get; set; }

        public string OriginalLanguage { get; set; }
        public double Popularity { get; set; }
        public bool Video { get; set; }
    }
}
