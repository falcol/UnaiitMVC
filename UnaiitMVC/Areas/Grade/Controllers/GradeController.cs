using App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnaiitMVC.Models;
using UnaiitMVC.Models.Grade;

namespace UnaiitMVC.Areas.Grade.Controllers
{
    [Area("Grade")]
    [Route("/Grade/[action]")]
    public class GradeController : Controller
    {
        private readonly UnaiitDbContext _context;

        public GradeController(UnaiitDbContext context)
        {
            _context = context;
        }

        // GET: Grade
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var unaiitDbContext = _context.Grade.Include(g => g.Faculty);
            return View(await unaiitDbContext.ToListAsync());
        }

        // GET: Grade/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var gradeTable = await _context.Grade
                .Include(g => g.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeTable == null)
            {
                return NotFound();
            }

            return View(gradeTable);
        }

        // GET: Grade/Create
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Creator");
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleName.Administrator)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity,Founded,Creator,FacultyId")] GradeTable gradeTable)
        {
            if (ModelState.IsValid)
            {
                gradeTable.Id = Guid.NewGuid();
                _context.Add(gradeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Creator", gradeTable.FacultyId);
            return View(gradeTable);
        }

        // GET: Grade/Edit/5
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var gradeTable = await _context.Grade.FindAsync(id);
            if (gradeTable == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Creator", gradeTable.FacultyId);
            return View(gradeTable);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleName.Administrator)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Capacity,Founded,Creator,FacultyId")] GradeTable gradeTable)
        {
            if (id != gradeTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var old = _context.Grade.Find(id);
                    // _context.Update(gradeTable);
                    _context.Entry(old).CurrentValues.SetValues(gradeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeTableExists(gradeTable.Id))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Creator", gradeTable.FacultyId);
            return View(gradeTable);
        }

        // GET: Grade/Delete/5
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var gradeTable = await _context.Grade
                .Include(g => g.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeTable == null)
            {
                return NotFound();
            }

            return View(gradeTable);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = RoleName.Administrator)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Grade == null)
            {
                return Problem("Entity set 'UnaiitDbContext.Grade'  is null.");
            }
            var gradeTable = await _context.Grade.FindAsync(id);
            if (gradeTable != null)
            {
                _context.Grade.Remove(gradeTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeTableExists(Guid id)
        {
            return (_context.Grade?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
