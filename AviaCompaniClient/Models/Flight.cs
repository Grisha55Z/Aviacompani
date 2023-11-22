using System;
using System.Collections.Generic;

namespace Aviacompani.Models;

public partial class Flight
{
    public int id { get; set; }

    public string? name { get; set; }

    public string? namber { get; set; }

    public TimeSpan? datetime { get; set; }
}
