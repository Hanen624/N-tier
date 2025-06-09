using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcTodoApp.Controllers
{
    public class HomeController : Controller
    {
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "تدرب على MVC Design Pattern", IsComplete = false },
            new TaskItem { Id = 2, Title = "تدرب على N-tier Architecture", IsComplete = false },
            new TaskItem { Id = 3, Title = "تدرب على استخدام git", IsComplete = false },
        };

        public IActionResult Index(int? editingTaskId)
        {
            ViewBag.EditingTaskId = editingTaskId;
            return View(tasks);
        }

        [HttpPost]
        public IActionResult AddTask(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                int newId = tasks.Max(t => t.Id) + 1;
                tasks.Add(new TaskItem { Id = newId, Title = title, IsComplete = false });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.IsComplete = true;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateTask(int id, string title)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null && !string.IsNullOrEmpty(title))
            {
                task.Title = title;
            }
            return RedirectToAction("Index");
        }
    }
}