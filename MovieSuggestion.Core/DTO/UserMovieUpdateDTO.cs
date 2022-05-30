using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.DTO
{
    public class UserMovieUpdateDTO
    {
        public ulong UserId { get; set; }

        public ulong MovieId { get; set; }

        public double Score { get; set; }
        public string Note { get; set; }
    }
}
