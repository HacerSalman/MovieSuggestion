using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.DTO
{
    public class UserMovieDTO
    {
        public ulong UserId { get; set; }
        public MovieDTO Movie { get; set; }
        public double Score { get; set; }
        public List<string> NoteList { get; set; }
    }
}
