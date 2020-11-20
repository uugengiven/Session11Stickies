using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session11Stickies.Models
{
    public class Sticky
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int SortOrder { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
