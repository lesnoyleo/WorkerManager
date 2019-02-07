using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using WorkerManager.Models;

namespace WorkerManager.Controllers
{
    public class CompanyController : Controller
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // GET: Company
        public ActionResult Index()
        {
            List<Company> companies = new List<Company>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlExpression = "SELECT * FROM Company";
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

                // если есть данные
                if (reader.HasRows)
                {
                    // построчно считываем данные 
                    while (reader.Read())
                    {
                        // заполняем коллецию рабоников из reader
                        companies.Add(new Company
                        {
                            id = reader.GetInt32(0),
                            name = reader.GetString(1),
                            companySize = reader.GetInt32(2),
                            format = reader.GetString(3),

                        });
                    }
                    reader.Close();
                }
                ViewBag.Companies = companies;
                return View();
            }
        }

        [HttpGet]
        public ActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult AddCompany(Company company)
        {
            string sqlExpression = "INSERT INTO Company VALUES ('" + company.name + "','" + company.companySize + "','" + company.format + "')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }
            return Redirect("/Company/Index");
        }

        [HttpGet]
        public ActionResult UpdateCompany(int id)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlExpression = "SELECT * FROM Company where id=" + id + " ";
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // построчно считываем данные 
                    while (reader.Read())
                    {
                        // заполняем коллецию рабоников из reader

                        ViewBag.id = reader.GetInt32(0);
                        ViewBag.name = reader.GetString(1);
                        ViewBag.companySize = reader.GetInt32(2);
                        ViewBag.format = reader.GetString(3);

                    }
                    reader.Close();
                }
                return View();
            }
        }

        [HttpPost]
        public RedirectResult UpdateCompany(Company company)
        {
            string sqlExpression = "UPDATE Company SET name='" + company.name + "' , companySize='" + company.companySize + "' ," + 
                " format='" + company.format+ "' where id='" + company.id + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }
            return Redirect("/Company/Index");
        }


        [HttpGet]
        public RedirectResult DeleteCompany(int id)
        {
            string sqlExpression = "Delete from Worker where id='" + id + "' ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }
            return Redirect("/Company/Index");

        }

        [HttpGet]
        public RedirectResult Cancel()
        {
            return Redirect("/Company/Index");
        }

    }
}