﻿using System;
using System.Collections.Generic;

namespace WebAPI.Models.Northwind;

public partial class Customer_and_Suppliers_by_City
{
    public string City { get; set; }

    public string CompanyName { get; set; }

    public string ContactName { get; set; }

    public string Relationship { get; set; }
}
