using Reading_RSS_feed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class NewsViewModel
    {
        public List<string> Sources { get; set; } = new List<string>() { "interfax", "habr", "все" };
        public string Source { get; set; }
        public string Sort { get; set; }
        public List<ItemNews> NewsList { get; set; } = new List<ItemNews>();
        public PageInfoViewModel PageInfo { get; set; } = new PageInfoViewModel();
    }
}
