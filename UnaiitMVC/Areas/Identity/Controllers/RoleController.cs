using App.Areas.Identity.Models.RoleViewModels;
using App.Data;
using App.ExtendMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using UnaiitMVC.Models;

namespace App.Areas.Identity.Controllers
{

    [Area("Identity")]
    [Route("/Role/[action]")]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UnaiitDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public RoleController(ILogger<RoleController> logger, RoleManager<IdentityRole> roleManager, UnaiitDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        //
        // GET: /Role/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var r = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
            var roles = new List<RoleModel>();
            foreach (var _r in r)
            {
                var claims = await _roleManager.GetClaimsAsync(_r);
                var claimsString = claims.Select(c => c.Type + "=" + c.Value);

                var rm = new RoleModel()
                {
                    Name = _r.Name,
                    Id = _r.Id,
                    Claims = claimsString.ToArray()
                };
                roles.Add(rm);
            }

            return View(roles);
        }

        // GET: /Role/Create
        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        [HttpPost, ActionName(nameof(Create))]
        [Authorize(Roles = RoleName.Administrator)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CreateRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var newRole = new IdentityRole(model.Name);
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa tạo role mới: {model.Name}";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(result);
            }
            return View();
        }

        // GET: /Role/Delete/roleid
        [HttpGet("{roleid}")]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> DeleteAsync(string roleid)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }
            return View(role);
        }

        // POST: /Role/Delete/1
        [HttpPost("{roleid}"), ActionName("Delete")]
        [Authorize(Roles = RoleName.Administrator)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmAsync(string roleid)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) return NotFound("Không tìm thấy role");

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa xóa: {role.Name}";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(result);
            }
            return View(role);
        }

        // GET: /Role/Edit/roleid
        [HttpGet("{roleid}")]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> EditAsync(string roleid, [Bind("Name")] EditRoleModel model)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }
            model.Name = role.Name;
            // model.role = role;

            ModelState.Clear();
            return View(model);

        }

        // POST: /Role/Edit/1
        [HttpPost("{roleid}"), ActionName("Edit")]
        [Authorize(Roles = RoleName.Administrator)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmAsync(string roleid, [Bind("Name")] EditRoleModel model)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }
            model.Name = role.Name;
            Console.WriteLine("MODEL " + ModelState.ToJson());
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            role.Name = model.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa đổi tên: {model.Name}";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(result);
            }

            return View(model);
        }
    }
}
