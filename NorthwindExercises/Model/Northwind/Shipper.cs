using System;
using System.Collections.Generic;

namespace NorthwindExercises.Model.Northwind;

public partial class Shipper
{
    public int ShipperID { get; set; }

    public string CompanyName { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
