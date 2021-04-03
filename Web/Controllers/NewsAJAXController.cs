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
    public class NewsAJAXController : Controller
    {
        private readonly RssDbContext _context;
        private int pageSize = 10;

        public NewsAJAXController(RssDbContext context)
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

            if (source == "interfax")
            {
                modelView.NewsList = _context.News.Where(e => e.Id.Contains(source)).ToList();
            }
            else if(source == "habr")
            {
                modelView.NewsList = _context.News.Where(e => e.Id.Contains(source)).ToList();
            }
            else
            {
                modelView.NewsList = _context.News.ToList();
            }

            if(sort == "sortByDate")
            {
                modelView.NewsList = modelView.NewsList.OrderByDescending(e => e.PublishDate).ToList();
            }
            else if(sort == "sortBySourse")
            {
                modelView.NewsList = modelView.NewsList.OrderBy(e => e.Id).ToList();
            }


            var count = modelView.NewsList.Count();
            var items =  modelView.NewsList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            modelView.NewsList = items;
            modelView.PageInfo = new PageInfoViewModel(count, page, pageSize);

            return PartialView(nameof(Index), modelView);
        }
    }
}
