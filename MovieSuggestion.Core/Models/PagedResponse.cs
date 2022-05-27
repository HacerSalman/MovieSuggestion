using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Models
{
    public class PagedResponse<T> where T : class, new()
    {
        public List<T> Result { get; set; }
        public Paging Paging { get; set; }
    }
}
