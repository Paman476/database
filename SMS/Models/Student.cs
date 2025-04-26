using System;
using System.Collections.Generic;

namespace SMS.Models;

public partial class Student
{
    public int Sid { get; set; }

    public string Sname { get; set; } = null!;

    public string? Contact { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
