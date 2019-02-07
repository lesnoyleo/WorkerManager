using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkerManager.Models
{
    // Структура объекта рабочего
    public class Worker
    {
        public int id { get; set; }

        public string middleName { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime employmentDate { get; set; }

        public string position { get; set; }

        public string company { get; set; }

        public string date { get; set; }   

    }
}
