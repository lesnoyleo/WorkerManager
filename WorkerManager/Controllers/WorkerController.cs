using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkerManager.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace WorkerManager.Controllers
{
    public class WorkerController : Controller
    {
        // Привязываем глобально значение connectionString к строке подключения в веб конфиге,
        // Для дальнейшего повсеместного использования в программе
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ActionResult Index()
        {
            // Создаем коллекцию для дальнейшего ее наполнения из бд
            List<Worker> workers = new List<Worker>();

            // Создаем и инициализируем подключение через спец конструктор с параметрами 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlExpression = "SELECT * FROM Worker";
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // Выполнение sql запроса и возвращение количество измененных записей
                var reader = command.ExecuteReader();

                // если есть данные
                if (reader.HasRows)
                {
                    // построчно считываем данные 
                    while (reader.Read())
                    {
                        // заполняем коллецию работников из reader
                        workers.Add(new Worker
                        {
                            id = reader.GetInt32(0),
                            middleName = reader.GetString(1),
                            firstName = reader.GetString(2),
                            lastName = reader.GetString(3),
                            employmentDate = reader.GetDateTime(4),
                            position = reader.GetString(5),
                            company = reader.GetString(6)
                        });
                    }
                    reader.Close();
                }
                ViewBag.Workers = workers;
                return View();
            }
        }

        [HttpGet]
        public ActionResult AddWorker()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult AddWorker(Worker worker)
        {
            string sqlExpression = "INSERT INTO Worker VALUES ('"+worker.middleName+ "','" + worker.firstName + "','" + worker.lastName + "" +
                "','" + worker.employmentDate + "','" + worker.position + "','" + worker.company + "')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }
            return Redirect("/Worker/Index");
        }

        [HttpGet]
        public ActionResult UpdateWorker(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlExpression = "SELECT * FROM Worker where id="+id+" ";
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ViewBag.id = reader.GetInt32(0);
                        ViewBag.middleName = reader.GetString(1);
                        ViewBag.firstName = reader.GetString(2);
                        ViewBag.lastName = reader.GetString(3);
                        ViewBag.employmentDate = reader.GetDateTime(4);
                        ViewBag.position = reader.GetString(5);
                        ViewBag.company = reader.GetString(6);
                    }
                    reader.Close();
                }
                return View();
            }
        }

        [HttpPost]
        public RedirectResult UpdateWorker(Worker worker)
        {
            string sqlExpression = "UPDATE Worker SET middleName='"+worker.middleName+ "' , firstName='" + worker.firstName + "' ," +
                " lastName='" + worker.lastName + "' , employmentDate='" + worker.employmentDate + "' ," +
                "position='" + worker.position + "' , company='" + worker.company + "' where id='" + worker.id + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }
            return Redirect("/Worker/Index");
        }


        [HttpGet]
        public RedirectResult DeleteWorker(int id)
        {
            string sqlExpression = "Delete from Worker where id='"+id+"' ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }
            return Redirect("/Worker/Index");

        }

        [HttpGet]
        public RedirectResult Cancel()
        {
            return Redirect("/Worker/Index");
        }


    }
}