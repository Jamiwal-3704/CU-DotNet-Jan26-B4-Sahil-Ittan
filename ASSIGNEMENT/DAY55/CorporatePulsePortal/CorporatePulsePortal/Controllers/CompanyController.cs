using CorporatePulsePortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorporatePulsePortal.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Dashboard()
        {
            // Simulating database data
            List<Employee> employees = new List<Employee>()
            {
                new Employee(1, "Sahil Ittan", "Software Engineer", 70000),
                new Employee(2, "Shivam Mourya", "Project Manager", 90000),
                new Employee(3, "Vikram Patel", "UI/UX Designer", 65000),
                new Employee(4, "Neha Verma", "QA Engineer", 60000)
            };

            // ViewBag → Daily announcement
            ViewBag.Announcement = "Team Meeting today at 4:00 PM in Conference Room.";

            // ViewData → Department information
            ViewData["DepartmentName"] = "Engineering Department";
            ViewData["ServerStatus"] = true;

            return View(employees);
        }
    }
}
