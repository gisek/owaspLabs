using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OwaspModel;

namespace OwaspDemo.Injection.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ShopDbContext _shopDbContext;

        public ProductsController(IConfiguration configuration, ShopDbContext shopDbContext)
        {
            _configuration = configuration;
            _shopDbContext = shopDbContext;
        }

        [HttpGet("products1")]
        public ActionResult<IEnumerable<string>> GetProducts1()
        {
            var id = HttpContext.Request.Query["id"];

            var connectionString = _configuration["ConnectionStrings:RootAccessDbConnectionString"];
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                var sql = $"SELECT * FROM Products WHERE IsAvailable = 1 AND Id = {id}";

                var products = new List<string>();

                using(var cmd = new SqlCommand(sql, conn))
                {
                    var reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        var productTableRow = $"Id: {reader["Id"]}, Name:{reader["Name"]}, Price:{reader["Price"]}, IsAvailable:{reader["IsAvailable"]}";

                        products.Add(productTableRow);
                    }
                }

                return products;
            }
        }
    }
}
