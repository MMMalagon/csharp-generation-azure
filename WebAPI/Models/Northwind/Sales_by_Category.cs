﻿using System;
using System.Collections.Generic;

namespace WebAPI.Models.Northwind;

public partial class Sales_by_Category
{
    public int CategoryID { get; set; }

    public string CategoryName { get; set; }

    public string ProductName { get; set; }

    public decimal? ProductSales { get; set; }
}
