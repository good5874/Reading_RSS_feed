using Reading_RSS_feed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Reading_RSS_feed
{
    public class RssParser
    {
        public static List<ItemNews> ParseRss(string url)
        {
            try
            {
                XDocument feed = XDocument.Load(url);

                var entries = from item in feed.Root.Descendants()
                              .First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new ItemNews
                              {
                                  Id = item.Elements().First(i => i.Name.LocalName == "guid").Value,

                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  PublishDate = Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value)
                              };
                return entries.ToList();
            }
            catch
            {
                return new List<ItemNews>();
            }
        }
    }
}
