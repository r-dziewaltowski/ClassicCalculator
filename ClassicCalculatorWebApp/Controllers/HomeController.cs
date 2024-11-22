using ClassicCalculator;
using Microsoft.AspNetCore.Mvc;

namespace ClassicCalculatorWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Calculator _calculator;

        public HomeController(Calculator calculator)
        {
            _calculator = calculator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PerformOperation(double num1, double num2, OperationType operation)
        {
            var result = _calculator.PerformOperation(num1, num2, operation);
            ViewBag.Result = result;
            return View("Index");
        }

        [HttpPost]
        public IActionResult PerformChainedOperation(double num2, OperationType operation)
        {
            var result = _calculator.PerformOperation(num2, operation);
            ViewBag.Result = result;
            return View("Index");
        }

        [HttpPost]
        public IActionResult Reset()
        {
            _calculator.Reset();
            ViewBag.Result = _calculator.LastResult;
            return View("Index");
        }
    }
}
