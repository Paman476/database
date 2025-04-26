using System;
using System.Collections.Generic;

namespace SMS.Models;

public partial class Registration
{
    public int Rid { get; set; }

    public int? Tid { get; set; }

    public string? Ccode { get; set; }

    public int? Sid { get; set; }

    public virtual Course? CcodeNavigation { get; set; }

    public virtual Student? SidNavigation { get; set; }

    public virtual Teacher? TidNavigation { get; set; }
}
