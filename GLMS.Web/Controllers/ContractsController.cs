using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GLMS.Web.Data;
using GLMS.Web.Models;
using GLMS.Web.Services;

namespace GLMS.Web.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly FileService _fileService;

        public ContractsController(ApplicationDbContext context, IWebHostEnvironment environment, FileService fileService)
        {
            _context = context;
            _environment = environment;
            _fileService = fileService;
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
        public IActionResult Create(Contract contract, IFormFile signedAgreement)
        {
            if (ModelState.IsValid)
            {
                if (signedAgreement != null)
                {
                    contract.SignedAgreementPath =
                        _fileService.UploadPdf(
                            signedAgreement,
                            _environment.WebRootPath);
                }

                _context.Contracts.Add(contract);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientId = new SelectList(_context.Clients, "Id", "Name");
            return View(contract);
        }

        public IActionResult Download(int id)
        {
            var contract = _context.Contracts.Find(id);

            if (contract == null || string.IsNullOrEmpty(contract.SignedAgreementPath))
                return NotFound();

            var filePath = Path.Combine(
                _environment.WebRootPath,
                contract.SignedAgreementPath.TrimStart('/'));

            return PhysicalFile(filePath, "application/pdf");
        }
    }
}