using Reading_RSS_feed.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Reading_RSS_feed
{
    public static class DataHelper
    {
        public static void AddNews(List<ItemNews> news)
        {
            using (RssDbContext dbContext = new RssDbContext())
            {
                dbContext.News.AddRange(news);
                dbContext.SaveChanges();
            }
        }
        public static ItemNews GetLastNews(string siteNews)
        {
            using (RssDbContext dbContext = new RssDbContext())
            {
                var intefaxNews = dbContext.News.OrderByDescending(e => e.PublishDate).Where(e => e.Link.Contains(siteNews));
                return intefaxNews.FirstOrDefault();
            }
        }
    }
}
