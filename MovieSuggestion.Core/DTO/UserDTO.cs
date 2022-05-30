using MovieSuggestion.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public EntityStatus.Values Status { get; set; }
        public long CreateTime { get; set; }
        public long UpdateTime { get; set; }

        public string Owner { get; set; }
        public string Modifier { get; set; }
    }
}
