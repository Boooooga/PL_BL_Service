using Microsoft.AspNetCore.Mvc;
using PL_BL_Service.BL;
using PL_BL_Service.Models;
using System.Diagnostics;

namespace PL_BL_Service.Controllers
{
    public class RoutesController : Controller
    {
        private readonly IBusinessService _businessService;

        public RoutesController(IBusinessService businessService)
        {
            _businessService = businessService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
