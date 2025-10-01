﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMiniShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title {  get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Category {  get; set; }
        public List<Rating>? Ratings { get; set; }
    }
}
