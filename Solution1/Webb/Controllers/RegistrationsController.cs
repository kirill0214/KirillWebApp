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

    }
}
