﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servicebus
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public decimal price { get; set; }
    }
}
