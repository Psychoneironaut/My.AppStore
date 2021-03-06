﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My.AppStore.Models
{
    public class ProductModel
    {
        public int? ID { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public IEnumerable<string> Images { get; set; }

        public string Description { get; set; }

        public ReviewModel[] Reviews { get; set; }
    }
}