using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reading_RSS_feed.Models;
using Web.Models;

namespace Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly RssDbContext _context;
        private int pageSize = 10;



        public NewsController(RssDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(new NewsViewModel() { Sources = new List<string> { "interfax", "habr","все" } });
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsList( string source, string sort, int page = 1)
        {
            NewsViewModel modelView = new NewsViewModel();
            modelView.Source = source;
            modelView.Sort = sort;

            IEnumerable<ItemNews> result;
            if (source == "interfax" || source == "habr")
            {
                result = _context.News.Where(e => e.Id.Contains(source));
            }
            else
            {
                result = _context.News;
            }
            if (sort == "sortByDate")
            {
                result = result.OrderByDescending(e => e.PublishDate);
            }
            else if (sort == "sortBySourse")
            {
                result = result.OrderBy(e => e.Id);
            }

            modelView.NewsList = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var count = result.Count();
            modelView.PageInfo = new PageInfoViewModel(count, page, pageSize);

            return View(nameof(Index), modelView);
        }
    }
}
