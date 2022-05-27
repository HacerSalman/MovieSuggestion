using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Models
{
    public class BaseResponse
    {
        public object Error { get; set; }
        public object Data { get; set; }
        public object Paging { get; set; }
    }
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class Paging
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}
