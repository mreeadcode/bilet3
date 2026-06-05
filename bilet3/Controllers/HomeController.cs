using System.Diagnostics;
using System.Threading.Tasks;
using bilet3.DAL;
using bilet3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bilet3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Person> persons = await _context.People.ToListAsync();
            return View(persons);
        }

    }
}
