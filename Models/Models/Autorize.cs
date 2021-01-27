using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class Autorize
    {
        public Role UserRole { get; }
        public Guid UserId { get; }

        public Autorize(Role role, Guid id)
        {
            UserRole = role;
            UserId = id;
        }
    }

    public enum Role
    {
        None,
        Admin,
        Director,
        Developer,
        Freelancer
    }
}
