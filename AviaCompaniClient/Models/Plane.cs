using System;
using System.Collections.Generic;

namespace Aviacompani.Models;

public partial class Plane
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Capacity { get; set; }

    public string? NumberOfPassengers { get; set; }
}
