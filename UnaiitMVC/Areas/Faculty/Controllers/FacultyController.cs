using App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnaiitMVC.Models;
using UnaiitMVC.Models.Faculty;

namespace UnaiitMVC.Areas.Faculty.Controllers
{
    [Area("Faculty")]
    [Route("/Faculty/[action]")]
    public class FacultyController : Controller
    {
        private readonly UnaiitDbContext _context;

        public FacultyController(UnaiitDbContext context)
        {
            _context = context;
        }

        // GET: Faculty
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var unaiitDbContext = _context.Faculty.Include(f => f.School);
            return View(await unaiitDbContext.ToListAsync());
        }

        // GET: Faculty/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Faculty == null)
            {
                return NotFound();
            }

            var facultyTable = await _context.Faculty
                .Include(f => f.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyTable == null)
            {
                return NotFound();
            }

            return View(facultyTable);
        }

        // GET: Faculty/Create
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public IActionResult Create()
        {
            ViewData["SchoolId"] = new SelectList(_context.School, "Id", "Name");
            return View();
        }

        // POST: Faculty/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity,Founded,Creator,SchoolId")] FacultyTable facultyTable)
        {
            if (ModelState.IsValid)
            {
                facultyTable.Id = Guid.NewGuid();
                _context.Add(facultyTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolId"] = new SelectList(_context.School, "Id", "Address", facultyTable.SchoolId);
            return View(facultyTable);
        }

        // GET: Faculty/Edit/5
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Faculty == null)
            {
                return NotFound();
            }

            var facultyTable = await _context.Faculty.FindAsync(id);
            if (facultyTable == null)
            {
                return NotFound();
            }
            ViewData["SchoolId"] = new SelectList(_context.School, "Id", "Address", facultyTable.SchoolId);
            return View(facultyTable);
        }

        // POST: Faculty/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Capacity,Founded,Creator,SchoolId")] FacultyTable facultyTable)
        {
            if (id != facultyTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var old = _context.Faculty.Find(id);
                    _context.Entry(old).CurrentValues.SetValues(facultyTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyTableExists(facultyTable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolId"] = new SelectList(_context.School, "Id", "Address", facultyTable.SchoolId);
            return View(facultyTable);
        }

        // GET: Faculty/Delete/5
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Faculty == null)
            {
                return NotFound();
            }

            var facultyTable = await _context.Faculty
                .Include(f => f.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyTable == null)
            {
                return NotFound();
            }

            return View(facultyTable);
        }

        // POST: Faculty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Faculty == null)
            {
                return Problem("Entity set 'UnaiitDbContext.Faculty'  is null.");
            }
            var facultyTable = await _context.Faculty.FindAsync(id);
            if (facultyTable != null)
            {
                var gradeTable = await _context.Grade.Where(g => g.FacultyId == id).ToListAsync();
                if (gradeTable != null)
                {
                    _context.Grade.RemoveRange(gradeTable);
                    // await _context.SaveChangesAsync();
                }
                _context.Faculty.Remove(facultyTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyTableExists(Guid id)
        {
            return (_context.Faculty?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
