using System;
using System.Collections.Generic;
using System.Text;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class Autorize
    {
        public bool IsAutentificated { get; set; }
        public Role AutorizeRole { get; set; }
    }

    public enum Role
    {
        None,
        Admin,
        Director,
        User
    }
}
