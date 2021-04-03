using Quartz;
using Reading_RSS_feed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reading_RSS_feed
{
    class ParseJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine($"Parse date: {DateTime.Now}");

                var interfaxList = RssParser.ParseRss("https://interfax.by/news/feed/");
                var habrList = RssParser.ParseRss("https://habr.com/ru/rss/all/all/");

                var lastInterfaxNews = DataHelper.GetLastNews("interfax");
                var lastHabrNews = DataHelper.GetLastNews("habr");

                List<ItemNews> interfaxListToSave = new List<ItemNews>();
                List<ItemNews> habrListToSave = new List<ItemNews>();

                if (lastInterfaxNews != null)
                {
                    interfaxListToSave = interfaxList.Where(e => e.PublishDate > lastInterfaxNews.PublishDate).ToList();
                }
                else
                {
                    interfaxListToSave = interfaxList;
                }

                if (lastHabrNews != null)
                {
                    habrListToSave = habrList.Where(e => e.PublishDate > lastHabrNews.PublishDate).ToList();
                }
                else
                {
                    habrListToSave = habrList;
                }

                var listNews = interfaxListToSave.Concat(habrListToSave).ToList();

                if (listNews.Count > 0)
                {
                    DataHelper.AddNews(listNews);
                }
                Console.WriteLine($"interfax news \n\tsaved: {interfaxListToSave.Count} \n\tread: {interfaxList.Count}");
                Console.WriteLine($"habr news \n\tsaved: {habrListToSave.Count} \n\tread: {habrList.Count}");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
