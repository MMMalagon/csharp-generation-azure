using System;
using System.Collections.Generic;

namespace WebAPI.Models.Northwind;

public partial class Territory
{
    public string TerritoryID { get; set; }

    public string TerritoryDescription { get; set; }

    public int RegionID { get; set; }

    public virtual Region Region { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
