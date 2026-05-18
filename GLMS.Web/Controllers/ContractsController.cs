using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GLMS.Web.Data;
using GLMS.Web.Models;

namespace GLMS.Web.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contracts = _context.Contracts.ToList();
            return View(contracts);
        }

        public IActionResult Create()
        {
            ViewBag.ClientId = new SelectList(_context.Clients, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Contracts.Add(contract);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientId = new SelectList(_context.Clients, "Id", "Name");
            return View(contract);
        }
    }
}