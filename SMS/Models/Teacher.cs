using System;
using System.Collections.Generic;

namespace SMS.Models;

public partial class Teacher
{
    public int Tid { get; set; }

    public string Tname { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
