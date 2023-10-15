using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using AutoMapper;
namespace TestAEB.Models
{
    /// <summary>
    /// Перечисление для статусов задачи
    /// </summary>
    public enum status
    {
        /// <summary>
        /// Создана
        /// </summary>
        created,
        /// <summary>
        /// В работе
        /// </summary>
        in_work,
        /// <summary>
        /// Выполнена
        /// </summary>
        completed
    }
    /// <summary>
    /// Модель данных для задачи
    /// </summary>
    public class ToDoModel
    {
        /// <summary>
        /// task id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Заголовок задачи
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Описание задачи
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Выполнить до даты
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime DueTime { get; set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? CompletionTime { get; set; }

        /// <summary>
        /// Статус выполнения задачи
        /// </summary>
        [Required]
        public status Status { get; set; }
    }
}
