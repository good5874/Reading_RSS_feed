using System;

namespace Reading_RSS_feed.Models
{
    public class ItemNews
    {
        public string Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
