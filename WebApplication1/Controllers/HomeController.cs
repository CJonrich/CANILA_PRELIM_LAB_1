using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Task 1: Variables & Data Types
            string studentName = "Jonrich Canilao";
            int score = 87;
            bool isPassed = (score >= 75);
            DateTime examDate = DateTime.Now;
            decimal tuitionFee = 15500.75m;

            ViewBag.StudentName = studentName;
            ViewBag.Score = score;
            ViewBag.IsPassed = isPassed;
            ViewBag.ExamDate = examDate;
            ViewBag.TuitionFee = tuitionFee;

            // Task 2: Operators and Control Structures
            string grade;
            if (score >= 90)
                grade = "A";
            else if (score >= 80)
                grade = "B";
            else if (score >= 75)
                grade = "C";
            else
                grade = "F";

            string message = isPassed ? "Congratulations, you passed!" : "Better luck next time.";
            ViewBag.Grade = grade;
            ViewBag.Message = message;

            // Task 3: Loops and Collections
            string[] courses = { "Web Systems", "OOP", "DBMS", "UI/UX", "Networking" };
            string courseList = string.Join(", ", courses);
            int courseCount = courses.Length;
            ViewBag.CourseList = courseList;
            ViewBag.CourseCount = courseCount;

            // Task 4: Methods
            decimal netFee = ComputeNetFee(tuitionFee, 10);
            ViewBag.NetFee = netFee;

            // Bonus: Format today’s date
            ViewBag.Today = DateTime.Now.ToString("MMMM dd, yyyy");

            // Task 5: Create Student object
            Student student = new Student
            {
                Name = "Jonrich Canilao",
                Age = 21,
                Course = "BSIT - Web Systems"
            };
            ViewBag.Student = student;

            // Task 6: List of Students
            List<Student> students = new List<Student>
            {
                new Student { Name = "Maria Santos", Age = 20, Course = "BSIT" },
                new Student { Name = "Pedro Ramirez", Age = 21, Course = "BSIT" },
                new Student { Name = "Angelica Reyes", Age = 22, Course = "BSIT" }
            };
            ViewBag.Students = students;

            return View();
        }

        // Task 4: Method for net fee
        private decimal ComputeNetFee(decimal tuition, decimal discountPercent)
        {
            return tuition - (tuition * discountPercent / 100);
        }

        // Task 7: Rename Privacy to AboutMe
        public IActionResult AboutMe()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
