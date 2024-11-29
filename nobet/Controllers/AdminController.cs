using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using emergencyProject.DataDb;
using emergencyProject.Models;

namespace nobet.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor ekleniyor
        public AdminController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult AddShift()
        {
            var assistants = _context.Assistants.ToList();
            var departments = _context.Departments.ToList();
            ViewBag.Assistants = assistants;
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public IActionResult AddShift(Shift shift)
        {
            if (ModelState.IsValid)
            {
                _context.Shifts.Add(shift);
                _context.SaveChanges();
                return RedirectToAction("ShiftList");
            }

            // Eğer model hatalıysa, Assistants ve Departments verilerini tekrar ViewBag'e ekle.
            ViewBag.Assistants = _context.Assistants.ToList();
            ViewBag.Departments = _context.Departments.ToList();
            return View(shift); // Modeli geri gönderir
        }

        public IActionResult ShiftList()
        {
            var shifts = _context.Shifts
                .Include(s => s.Assistant)
                .Include(s => s.Department)
                .ToList(); // Tüm shift'leri ilgili ilişkileriyle getir

            return View(shifts);
        }
    }
}
