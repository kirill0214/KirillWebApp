using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Infrastructure.Data;
namespace Webb.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Registrations
        public async Task<IActionResult> Index()
        {
            var registrations = _context.Registrations.Include(r => r.Client);
            return View(await registrations.ToListAsync());
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            var clients = _context.Clients.ToList();
            ViewBag.Clients = new SelectList(clients, "Id", "FullName");
            return View();
        }

        // POST: Registrations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Time,ClientId")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clients = new SelectList(_context.Clients.ToList(), "Id", "FullName", registration.ClientId);
            return View(registration);
        }
    }
}
