using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Calendarro.ViewComponents
{
    public class CalendarComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int number = 1)
        {
            ViewBag.MonthNumber = number;

            //var aaa = new List<string>();

            //for (int i = 0; i < 3; i++)
            //{
            //    aaa.Add($"calendar{i}");
            //}

            //ViewBag.RenderNumber = aaa;

            return View();
        }
    }
}
