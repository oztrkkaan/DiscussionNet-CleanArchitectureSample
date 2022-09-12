using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionNet.Domain.Entities
{
    public class FeedItem
    {
        public string Subject { get; set; }
        public int ThreadCount { get; set; }
        public int Url { get; set; }
        public DateTime LastThreadCreationDate { get; set; }
    }
}
