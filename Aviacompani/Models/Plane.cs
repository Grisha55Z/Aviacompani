using System;
using System.Collections.Generic;

namespace Aviacompani.Models;

public partial class Plane
{
    public int id { get; set; }

    public string? name { get; set; }

    public string? capacity { get; set; }

    public string? numberOfPassengers { get; set; }
}
