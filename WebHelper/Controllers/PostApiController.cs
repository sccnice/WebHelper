using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebHelper.Models;

namespace WebHelper.Controllers
{
    public class PostApiController : Controller
    {
        private string _host = "http://qlw.data.test.com/";
        public IActionResult Index()
        {
            return View();
        }
        public string Post(string posturl,string postData) {
            string requestResult=   InterfaceProxy.GetResult(this.HttpContext.Request,_host+ posturl,
                "application/json", "application/json", "post", postData);
            return requestResult;
        }
    }
}