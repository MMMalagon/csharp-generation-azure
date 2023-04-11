using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplicationDemo.Models;

namespace WebApplicationDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly NorthwindModel _context;

        public EmployeesController(NorthwindModel context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.ToList());
        }
    }
}
