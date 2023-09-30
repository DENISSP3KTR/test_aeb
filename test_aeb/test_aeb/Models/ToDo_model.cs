using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
namespace test_aeb.Models
{
    public enum Status
    {
        [Description("Создана")]
        created,
        [Description("В работе")]
        in_work,
        [Description("Завершена")]
        completed
    }
    public class ToDo_model
    {
        [Key]
        public int Id { get; set; }
        //Заголовок задачи
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        //Описание задачи
        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        //Выполнить до даты
        [Required]
        [DataType(DataType.Date)]
        public DateTime Due_Time { get; set; }

        //Дата создания задачи
        [Required]
        [DataType(DataType.Date)]
        public DateTime Create_Time { get; set; }

        //Дата завершения
        [Required]
        [DataType(DataType.Date)]
        public DateTime Completion_Time { get; set; }

        //Статус выполнения задачи
        [Required]
        public Status status { get; set; }
        [NotMapped]
        public string StatusAccess
        {
            get
            {
                return DescriptionAttributes<Status>.RetrieveAttributesReverse()[status.ToString()];
            }
        }
        public override string ToString()
        {
            return Title;
        }
    }
}
