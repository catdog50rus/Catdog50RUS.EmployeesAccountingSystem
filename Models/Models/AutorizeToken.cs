using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    /// <summary>
    /// Модель токена авторизации
    /// </summary>
    public class AutorizeToken
    {
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public Role UserRole { get; }
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; }

        public AutorizeToken(Role role, Guid id)
        {
            UserRole = role;
            UserId = id;
        }
    }

    /// <summary>
    /// Список ролей
    /// </summary>
    public enum Role
    {
        None,
        Admin,
        Director,
        Developer,
        Freelancer
    }
}
