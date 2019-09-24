using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreateIGuess.Models;
using CreateIGuess.Data;
using Microsoft.EntityFrameworkCore;

namespace CreateIGuess.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly LasteDbContext _dataContext;

        public HomeController(IMessageService messageService,
                                LasteDbContext dataContext)
        {
            _messageService = messageService;
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            _messageService.Send("Hello world!");

            return View();
        }

        public IActionResult Invoices()
        {
            var model = _dataContext.Invoices.ToList();
            return View(TestData.Invoices);
        }

        public async Task<IActionResult> Invoice(int id)
        {
            var invoice = await _dataContext
                            .Invoices
                            .Include(i=>i.Lines)
                           .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        public IActionResult Test(int id)
        {
            var model = new TestModel();
            model.Id = id;
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
