﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public int LineId { get; set; }
    }
}
