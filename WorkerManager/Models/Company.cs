using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkerManager.Models
{
    // Структура объекта компании
    public class Company
    {
        public int id { get; set; }

        public string name { get; set; }

        public int companySize { get; set; }

        public string format { get; set; }

    }
}