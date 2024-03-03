using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            Dictionary<int, string?> data = GetDb();

            return PartialView("DataPartial", data);
        }

        [HttpGet]
        public Dictionary<int, string?> GetDb()
        {
            var connectionString = _configuration.GetConnectionString("MyDb");
            _logger.LogInformation($"ConnectionString: {connectionString}");

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM MyTable";

            using var reader = command.ExecuteReader();

            var data = new Dictionary<int, string?>();
            while (reader.Read())
            {
                data[reader.GetInt32(0)] = reader.GetString(1);
            }
            return data;
        }

        [HttpPost]
        public void PostData(string text)
        {
            var connectionString = _configuration.GetConnectionString("MyDb");
            _logger.LogInformation($"ConnectionString: {connectionString}");

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO VALUES ('{text}')";
        }

        public IActionResult Privacy()
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
