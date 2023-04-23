﻿using System;
using System.Collections.Generic;

namespace WebAPI.Models.Northwind;

public partial class Region
{
    public int RegionID { get; set; }

    public string RegionDescription { get; set; }

    public virtual ICollection<Territory> Territories { get; } = new List<Territory>();
}