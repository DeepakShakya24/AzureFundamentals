﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangyWebFunc.Model
{
    public class SalesModel
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Designation { get; set; }
    }
}
