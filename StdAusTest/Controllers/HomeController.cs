using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StdAusTest.Models;
using Newtonsoft.Json;
using System.IO;

namespace StdAusTest.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		public Root LoadJson()
		{
			using (StreamReader r = new StreamReader("MOCK_DATA.json"))
			{
				string json = r.ReadToEnd();
				var Employee = JsonConvert.DeserializeObject<Root>(json);
				return Employee;
			}
		}
		public IActionResult Index()
		{
			dynamic array = LoadJson();
			return View(array);
		}

		[HttpPost]
		public IActionResult AddEmployee(Result Employee)
		{
			var array = LoadJson();
			Employee.id = array.result.Count + 1;
			array.result.Add(Employee);
			String json = JsonConvert.SerializeObject(array);
			System.IO.File.WriteAllText("MOCK_DATA.json", json);
			array = LoadJson();
			return View("Index",array);
		}
	}
}
