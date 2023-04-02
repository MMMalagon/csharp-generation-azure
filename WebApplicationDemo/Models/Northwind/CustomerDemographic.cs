﻿using System;
using System.Collections.Generic;

namespace WebApplicationDemo.Models.Northwind;

public partial class CustomerDemographic
{
    public string CustomerTypeID { get; set; }

    public string CustomerDesc { get; set; }

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
}