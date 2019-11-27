using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OwaspModel;

namespace OwaspDemo.Injection.Controllers {
    [ApiController]
    public class TemplateProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ShopDbContext _shopDbContext;

        public TemplateProductsController(IConfiguration configuration, ShopDbContext shopDbContext)
        {
            _configuration = configuration;
            _shopDbContext = shopDbContext;
        }

        [HttpGet("template/products1")]
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

        [HttpGet("template/products2")]
        public ActionResult<IEnumerable<string>> GetProducts2()
        {
            var id = HttpContext.Request.Query["id"];

            var connectionString = _configuration["ConnectionStrings:RootAccessDbConnectionString"];
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var sql = "SELECT * FROM Products WHERE IsAvailable = 1 AND Id = @Id";

                var products = new List<string>();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id.ToString();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var productTableRow = $"Id: {reader["Id"]}, Name:{reader["Name"]}, Price:{reader["Price"]}, IsAvailable:{reader["IsAvailable"]}";

                        products.Add(productTableRow);
                    }
                }

                return products;
            }
        }

        [HttpGet("template/getProductsByIdSafe")]
        public ActionResult<IEnumerable<string>> GetProductsByIdSafe()
        {
            var id = HttpContext.Request.Query["id"];

            var connectionString = _configuration["ConnectionStrings:RootAccessDbConnectionString"];
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var sql = "GetProducts";

                var products = new List<string>();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id.ToString();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var productTableRow = $"Id: {reader["Id"]}, Name:{reader["Name"]}, Price:{reader["Price"]}, IsAvailable:{reader["IsAvailable"]}";

                        products.Add(productTableRow);
                    }
                }

                return products;
            }
        }

        [HttpGet("template/getProductsByNameSafe")]
        public ActionResult<IEnumerable<string>> GetProductsByNameSafe()
        {
            var name = HttpContext.Request.Query["name"];

            var connectionString = _configuration["ConnectionStrings:RootAccessDbConnectionString"];
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var sql = "GetProductsByName";

                var products = new List<string>();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name.ToString();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var productTableRow = $"Id: {reader["Id"]}, Name:{reader["Name"]}, Price:{reader["Price"]}, IsAvailable:{reader["IsAvailable"]}";

                        products.Add(productTableRow);
                    }
                }

                return products;
            }
        }

        [HttpGet("template/productsByNameUnsafe")]
        public ActionResult<IEnumerable<string>> GetProductsByNameUnsafe()
        {
            var name = HttpContext.Request.Query["name"];

            var connectionString = _configuration["ConnectionStrings:RootAccessDbConnectionString"];
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var sql = "GetProductsUnsafe";

                var products = new List<string>();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name.ToString();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var productTableRow = $"Id: {reader["Id"]}, Name:{reader["Name"]}, Price:{reader["Price"]}, IsAvailable:{reader["IsAvailable"]}";

                        products.Add(productTableRow);
                    }
                }

                return products;
            }
        }

        [HttpGet("template/productsByNameSecured")]
        public ActionResult<IEnumerable<string>> GetProductsByNameSecured()
        {
            var name = HttpContext.Request.Query["name"];

            var connectionString = _configuration["ConnectionStrings:RootAccessDbConnectionString"];
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var sql = "GetProductsSecured";

                var products = new List<string>();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name.ToString();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var productTableRow = $"Id: {reader["Id"]}, Name:{reader["Name"]}, Price:{reader["Price"]}, IsAvailable:{reader["IsAvailable"]}";

                        products.Add(productTableRow);
                    }
                }

                return products;
            }
        }

        [HttpGet("template/productsWithOrm")]
        public ActionResult<IEnumerable<string>> GetProductsWithOrm()
        {
            var id = int.Parse(HttpContext.Request.Query["id"]);

            var products = _shopDbContext.Products
                .Where(product => product.Id == id)
                .Select(product => $"Id: {product.Id}, Name:{product.Name}, Price:{product.Price}, IsAvailable:{product.IsAvailable}")
                .ToList();

            return products;
        }
    }
}