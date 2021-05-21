using System;
using WebApplication2.Models;
using WebApplication2.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class CalculateController : Controller
    {
        private readonly ICalculateService calculateService;
        public CalculateController(ICalculateService calculateService)
        {
            this.calculateService = calculateService;
        }

        public ActionResult Manual()
        {
            if (this.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                String input1 = this.Request.Form["Number1"];
                String input2 = this.Request.Form["Number2"];
                bool error = false;

                int? number1 = null;
                int? number2 = null;

                if (String.IsNullOrEmpty(input1))
                {
                    this.ViewBag.Number1Null = "First number is NULL";
                    error = true;
                }
                else
                {
                    number1 = Convert.ToInt32(input1);
                }

                if (String.IsNullOrEmpty(input2))
                {
                    this.ViewBag.Number2Null = "Second number is NULL";
                    error = true;
                }
                else
                {
                    number2 = Convert.ToInt32(input2);
                }

                String operation = this.Request.Form["Operation"];

                if (number2 == 0 && operation == "/")
                {
                    this.ViewBag.DividedByZero = "Division by zero";
                    error = true;
                }

                this.ViewBag.Number1 = number1;
                this.ViewBag.Number2 = number2;
                this.ViewBag.Operation = operation;

                if (error)
                {
                    return this.View();
                }

                int result = 0;
                switch (operation)
                {
                    case "+":
                        result = calculateService.Summ(number1.Value, number2.Value); break;
                    case "-":
                        result = calculateService.Subtraction(number1.Value, number2.Value); break;
                    case "*":
                        result = calculateService.Multiplication(number1.Value, number2.Value); break;
                    case "/":
                        result = calculateService.Division(number1.Value, number2.Value); break;
                }

                var resultModel = new CalculateViewModel
                {
                    ResultNumber = result
                };

                return this.View("Manual", resultModel);

            }

            return this.View();
        }

        public ActionResult ManualWithSeparateHandlers()
        {
            return this.View();
        }

        [HttpPost, ActionName("ManualWithSeparateHandlers")]
        [ValidateAntiForgeryToken]
        public ActionResult ManualWithSeparateHandlersConfirm()
        {
            String input1 = this.Request.Form["Number1"];
            String input2 = this.Request.Form["Number2"];
            bool error = false;

            int? number1 = null;
            int? number2 = null;

            if (String.IsNullOrEmpty(input1))
            {
                this.ViewBag.Number1Null = "First number is NULL";
                error = true;
            }
            else
            {
                number1 = Convert.ToInt32(input1);
            }

            if (String.IsNullOrEmpty(input2))
            {
                this.ViewBag.Number2Null = "Second number is NULL";
                error = true;
            }
            else
            {
                number2 = Convert.ToInt32(input2);
            }

            String operation = this.Request.Form["Operation"];

            if (number2 == 0 && operation == "/")
            {
                this.ViewBag.DividedByZero = "Division by zero";
                error = true;
            }

            this.ViewBag.Number1 = number1;
            this.ViewBag.Number2 = number2;
            this.ViewBag.Operation = operation;

            if (error)
            {
                return this.View();
            }

            int result = 0;
            switch (operation)
            {
                case "+":
                    result = calculateService.Summ(number1.Value, number2.Value); break;
                case "-":
                    result = calculateService.Subtraction(number1.Value, number2.Value); break;
                case "*":
                    result = calculateService.Multiplication(number1.Value, number2.Value); break;
                case "/":
                    result = calculateService.Division(number1.Value, number2.Value); break;
            }

            var resultModel = new CalculateViewModel
            {
                ResultNumber = result
            };

            return this.View("ManualWithSeparateHandlers", resultModel);
        }

        public ActionResult ModelBindingInParameters()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModelBindingInParameters(int? Number1, int? Number2, string operation)
        {
            bool error = false;

            int number1 = 0, number2 = 0;

            if (Number1 == null)
            {
                this.ViewBag.Number1Null = "First number is NULL";
                error = true;
            }

            if (Number2 == null)
            {
                this.ViewBag.Number2Null = "Second number is NULL";
                error = true;
            }

            number1 = Convert.ToInt32(Number1);
            number2 = Convert.ToInt32(Number2);

            if (Number2 == 0 && operation == "/")
            {
                this.ViewBag.DividedByZero = "Division by zero";
                error = true;
            }

            this.ViewBag.Number1 = number1;
            this.ViewBag.Number2 = number2;
            this.ViewBag.Operation = operation;

            if (error)
            {
                return this.View();
            }

            int result = 0;
            switch (operation)
            {
                case "+":
                    result = calculateService.Summ(number1, number2); break;
                case "-":
                    result = calculateService.Subtraction(number1, number2); break;
                case "*":
                    result = calculateService.Multiplication(number1, number2); break;
                case "/":
                    result = calculateService.Division(number1, number2); break;
            }

            var resultModel = new CalculateViewModel
            {
                ResultNumber = result
            };

            return this.View("ModelBindingInParameters", resultModel);
        }

        public ActionResult ModelBindingInSeparateModel()
        {
            return this.View(new CalculateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModelBindingInSeparateModel(CalculateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (model.Number2 == 0 && model.Operation == "/")
                {
                    this.ModelState.AddModelError("Number2", "Division by zero is forbidden");
                    return this.View(model);
                }

                int? result = null;
                switch (model.Operation)
                {
                    case "+":
                        result = calculateService.Summ(model.Number1.Value, model.Number2.Value); break;
                    case "-":
                        result = calculateService.Subtraction(model.Number1.Value, model.Number2.Value); break;
                    case "*":
                        result = calculateService.Multiplication(model.Number1.Value, model.Number2.Value); break;
                    case "/":
                        result = calculateService.Division(model.Number1.Value, model.Number2.Value); break;
                }

                var resultModel = new CalculateViewModel()
                {
                    Number1 = model.Number1,
                    Number2 = model.Number2,
                    Operation = model.Operation,
                    ResultNumber = result
                };

                return this.View("ModelBindingInSeparateModel", resultModel);
            }

            return this.View(model);
        }
    }
}
