using System.Threading.Tasks;
using bilet3.Areas.Admin.ViewModels.Person;
using bilet3.DAL;
using bilet3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bilet3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PeopleController : Controller
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Person> people = await _context.People.ToListAsync();
            return View(people);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreatePersonVM vm)
        {
            if(!ModelState.IsValid)
            {
                    return View(vm);
            }

            

            bool isExist = await _context.People.AnyAsync(x => x.Fullname == vm.Fullname);

            Person person = new Person
            {
                Fullname = vm.Fullname,
                Designation = vm.Designation,
            };

            

            return RedirectToAction(nameof(Index));
        }

    }
}
