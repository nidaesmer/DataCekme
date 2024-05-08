using DataCekme.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataCekme.Controllers
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
            SqlConnection conn = new SqlConnection("server=.; database=Arkadaslar; Trusted_Connection=True");
            SqlCommand cmd = new SqlCommand("select Id,FullName,Phone,Address from Arkadaslar",conn);
             
            List<Arkadaslar> Arkadas= new List<Arkadaslar>();
            
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Arkadas.Add(
                    new Arkadaslar
                    {
                        Id = (int)dr["Id"],
                        FullName = (string)dr["FullName"],
                        Address = (string)dr["Address"],
                        Phone = (string)dr["Phone"],
                    });
            }
            conn.Close();

            return View(Arkadas);
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
