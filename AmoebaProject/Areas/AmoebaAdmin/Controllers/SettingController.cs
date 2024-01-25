using AmoebaProject.Areas.ViewModels.Setting;
using AmoebaProject.DAL;
using AmoebaProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmoebaProject.Areas.AmoebaAdmin.Controllers
{
    [Area("AmoebaAdmin")]
    [Authorize(Roles = "Admin")]

    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return View(settings);
        }
        public async Task<IActionResult> Update(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            Setting setting = await _context.Settings.FirstOrDefaultAsync(c => c.Id == id);
            if (setting == null)
            {
                return NotFound();
            }
            UpdateSettingVM settingVM = new UpdateSettingVM
            {
                Value = setting.Value,
            };
            return View(settingVM); 
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateSettingVM settingVM)
        {
            if (!ModelState.IsValid)
            {
                return View(settingVM);
            }
            Setting setting = await _context.Settings.FirstOrDefaultAsync(c => c.Id == id);
            setting.Value= settingVM.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Setting");

        }
    }
}
