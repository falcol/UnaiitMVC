using App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnaiitMVC.Models;
using UnaiitMVC.Models.School;

namespace UnaiitMVC.Areas.School.Controllers
{
    [Area("School")]
    [Route("/School/[action]")]
    public class SchoolController : Controller
    {
        private readonly UnaiitDbContext _context;

        public SchoolController(UnaiitDbContext context)
        {
            _context = context;
        }

        // GET: School
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.School != null ?
                        View(await _context.School.ToListAsync()) :
                        Problem("Entity set 'UnaiitDbContext.School'  is null.");
        }

        // GET: School/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.School == null)
            {
                return NotFound();
            }

            var schoolTable = await _context.School
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolTable == null)
            {
                return NotFound();
            }

            return View(schoolTable);
        }

        // GET: School/Create
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: School/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Founded,Capacity")] SchoolTable schoolTable)
        {
            if (ModelState.IsValid)
            {
                schoolTable.Id = Guid.NewGuid();
                _context.Add(schoolTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolTable);
        }

        // GET: School/Edit/5
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.School == null)
            {
                return NotFound();
            }

            var schoolTable = await _context.School.FindAsync(id);
            if (schoolTable == null)
            {
                return NotFound();
            }
            return View(schoolTable);
        }

        // POST: School/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleName.Administrator)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,Founded,Capacity")] SchoolTable schoolTable)
        {
            if (id != schoolTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolTableExists(schoolTable.Id))
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
            return View(schoolTable);
        }

        // GET: School/Delete/5
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.School == null)
            {
                return NotFound();
            }

            var schoolTable = await _context.School
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolTable == null)
            {
                return NotFound();
            }

            return View(schoolTable);
        }

        // POST: School/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.School == null)
            {
                return Problem("Entity set 'UnaiitDbContext.School'  is null.");
            }
            var schoolTable = await _context.School.FindAsync(id);
            if (schoolTable != null)
            {
                var facultyTable = await _context.Faculty.Where(f => f.SchoolId == id).ToListAsync();
                if (facultyTable != null)
                {
                    foreach (var faculty in facultyTable)
                    {
                        var gradeTable = await _context.Grade.Where(g => g.FacultyId == faculty.Id).ToListAsync();
                        if (gradeTable != null)
                        {
                            _context.Grade.RemoveRange(gradeTable);
                        }
                        _context.Faculty.Remove(faculty);
                    }
                }
                _context.School.Remove(schoolTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolTableExists(Guid id)
        {
            return (_context.School?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
