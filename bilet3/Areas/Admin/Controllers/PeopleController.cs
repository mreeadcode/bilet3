using System.Threading.Tasks;
using bilet3.Areas.Admin.Utilities.Enums;
using bilet3.Areas.Admin.Utilities.FileValidator;
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
        private readonly IWebHostEnvironment _env;

        public PeopleController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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


            if (isExist)
            {
                ModelState.AddModelError("Fullname", "Fullname artiq movcuddur");
                return View(vm);
            }

            if (!vm.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type duzgun secilmeyib!!");
                return View(vm);
            }

            if(!vm.Photo.CheckFileSize(5, FileSizeType.MB)){
                ModelState.AddModelError("Photo", "File olcusu duzgun deyil!!");
                return View(vm.Photo);
            }




            Person person = new Person
            {
                Fullname = vm.Fullname,
                Designation = vm.Designation,
                Image = await vm.Photo.CraeteFile(_env.WebRootPath, "assets", "images")
            };

            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();

            

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1)
            {
                return BadRequest();
            }

            Person person = await _context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person is null)
            {
                return NotFound();
            }

            UpdatePersonVM vm = new UpdatePersonVM
            {
                Fullname = person.Fullname,
                Designation = person.Designation,
                Image = person.Image
            };

            return View(vm);
        }

        [HttpPost]


        public async Task<IActionResult> Update(UpdatePersonVM vm ,int? id)
        {
            Person person = await _context.People.FirstOrDefaultAsync(x => x.Id==id);

            if (person is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if(vm.Photo is not null)
            {
                if (!vm.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type duzgun secilmeyib!!");
                    return View(vm);
                }

                if (!vm.Photo.CheckFileSize(5, FileSizeType.MB))
                {
                    ModelState.AddModelError("Photo", "File olcusu duzgun deyil!!");
                    return View(vm.Photo);
                }

                person.Image.DeleteFile(_env.WebRootPath, "assets", "images");
                person.Image = await vm.Photo.CraeteFile(_env.WebRootPath, "assets", "images");

            }


            person.Fullname = vm.Fullname;
            person.Designation = vm.Designation;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1)
            {
                return BadRequest();
            }

            Person person = await _context.People.FirstOrDefaultAsync(x =>  x.Id == id);

            if(person is null)
            {
                return NotFound();
            }

            person.Image.DeleteFile(_env.WebRootPath, "assets", "images");
            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
