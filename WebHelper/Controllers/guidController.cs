using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebHelper.Models;

namespace WebHelper.Controllers
{
    public class guidController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetGuid() {
            ResultMsg rm = new ResultMsg(true);
            rm.Data = Guid.NewGuid();
            // JsonSerializerSettings jss = new JsonSerializerSettings();jss.a
            return Json(rm);
        }
    }
}