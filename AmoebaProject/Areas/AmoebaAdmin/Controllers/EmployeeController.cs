using AmoebaProject.Areas.ViewModels;
using AmoebaProject.Areas.ViewModels;
using AmoebaProject.DAL;
using AmoebaProject.Models;
using AmoebaProject.Utilities.Enums;
using AmoebaProject.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmoebaProject.Areas.AmoebaAdmin.Controllers
{
    [Area("AmoebaAdmin")]
    [Authorize(Roles = "Admin")]

    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            int count = await _context.Employees.CountAsync();

            List<Employee> employees = await _context.Employees.Skip((page - 1) * 3).Take(3).ToListAsync();
            PaginationVM paginationVM = new PaginationVM
            {
                CurrentPage = page,
                TotalPage = Math.Ceiling((double)count / 3),
                Employees = employees
            };
            return View(paginationVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!employeeVM.Photo.ValidateFileType(FileHelper.Image))
            {
                ModelState.AddModelError("Photo", "File type is not matching please input Image");
                return View();
            }
            if (!employeeVM.Photo.ValidateFileSize(SizeHelper.mb))
            {
                ModelState.AddModelError("Photo", "File size is not matching");
                return View();
            }
            string filename = Guid.NewGuid().ToString() + employeeVM.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets", "img", "team", filename);
            FileStream file = new FileStream(path, FileMode.Create);
            await employeeVM.Photo.CopyToAsync(file);
            Employee employee = new Employee
            {
                Image = filename,
                Name = employeeVM.Name,
                Description = employeeVM.Description,
                Profession = employeeVM.Profession,
                FacebookLink = employeeVM.FacebookLink,
                InstagramLink = employeeVM.InstagramLink,
                XLink = employeeVM.XLink,
                LinkEdinLink = employeeVM.LinkEdinLink
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Employee");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            Employee employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Employee");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            Employee employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = employee.Name,
                Description = employee.Description,
                InstagramLink = employee.InstagramLink,
                LinkEdinLink = employee.LinkEdinLink,
                FacebookLink = employee.FacebookLink,
                Image = employee.Image,
                Profession = employee.Profession,
                XLink = employee.XLink
            };
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            Employee existed = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);
            if (employeeVM.Photo is not null)
            {
                if (!employeeVM.Photo.ValidateFileType(FileHelper.Image))
                {
                    ModelState.AddModelError("Photo", "File type is not matching please input Image");
                    return View(employeeVM);
                }
                if (!employeeVM.Photo.ValidateFileSize(SizeHelper.mb))
                {
                    ModelState.AddModelError("Photo", "File size is not matching");
                    return View(employeeVM);
                }
                string filename = Guid.NewGuid().ToString() + employeeVM.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets", "img", "team", filename);
                FileStream file = new FileStream(path, FileMode.Create);
                await employeeVM.Photo.CopyToAsync(file);
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "img", "team");
                existed.Image = filename;
            }
            existed.Profession = employeeVM.Profession;
            existed.LinkEdinLink = employeeVM.LinkEdinLink;
            existed.XLink = employeeVM.XLink;
            existed.InstagramLink = employeeVM.InstagramLink;
            existed.FacebookLink = employeeVM.FacebookLink;
            existed.Name = employeeVM.Name;
            existed.Description = employeeVM.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Employee");
        }
    }
}
