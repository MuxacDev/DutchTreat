using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
	public class AppController : Controller
	{
        private readonly IMailService _mailService;
        private readonly IDutchRepository repository;
        //private readonly DutchContext _context;

        public AppController(IMailService mailService, /*DutchContext context,*/ IDutchRepository repository)
        {
            _mailService = mailService;
            this.repository = repository;
            //_context = context;
        }

		public IActionResult Index()
		{
            //var results = _context.Products.ToList();
			return View();
		}

        [HttpGet("contact")]
        public IActionResult Contact()
        {			

            //throw new InvalidOperationException("Bad things happen");

            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send the email
                _mailService.SendMessage("shawn@wildermuth.com",model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message} ");
                ViewBag.UserMessage = "Mail Sent";
            }
           /* else
            {
                //Show the error
            }*/
            return View();

        }
        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            var results = repository.GetAllProducts();
            /*var results = _context.Products
                .OrderBy(p => p.Category)
                .ToList();*/
            /*var results = from p in _context.Products
                          orderby p.Category
                          select p;*/
            return View(results.ToList());
        }
    }
}