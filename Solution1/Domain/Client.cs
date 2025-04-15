using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain
{
    public class Client
    {
        public int Id { get; set; } // Первичный ключ
        public string Name { get; set; }
        public string LName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public string FullName => $"{LName} {Name}";
    }
}

