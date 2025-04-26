using System;
using System.Collections.Generic;

namespace SMS.Models;

public partial class Course
{
    public string CCode { get; set; } = null!;

    public string CName { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
