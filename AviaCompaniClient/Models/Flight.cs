using System;
using System.Collections.Generic;

namespace Aviacompani.Models;

public partial class Flight
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Namber { get; set; }

    public TimeSpan? Datetime { get; set; }
}
