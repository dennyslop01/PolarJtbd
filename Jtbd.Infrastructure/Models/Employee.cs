using System;
using System.Collections.Generic;

namespace Jtbd.Infrastructure.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int EmployeeRol { get; set; }

    public int? DeparmentsId { get; set; }

    public int StatusEmployee { get; set; }

    public virtual Deparment? Deparments { get; set; }
}
