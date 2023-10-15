using System.ComponentModel.DataAnnotations;

namespace TestAEB.Models
{
    /// <summary>
    /// Модель данных пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя (логин)
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Хеш пароля пользователя
        /// </summЫary>
        [Required]
        public byte[] PasswordHash { get; set; }
        /// <summary>
        /// Salt для пароля пользователя
        /// </summary>
        [Required]
        public byte[] PasswordSalt {  get; set; }
    }
}
