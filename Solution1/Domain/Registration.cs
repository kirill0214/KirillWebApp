﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Registration
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }
        public int ClientId { get; set; }
        
        public Client? Client { get; set; }


    }
}
