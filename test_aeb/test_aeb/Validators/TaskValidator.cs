using FluentValidation;
using TestAEB.Models;

namespace TestAEB.Validators
{
    /// <summary>
    /// Validator for the ToDoModel
    /// </summary>
    public class TaskValidator : AbstractValidator<ToDoModel>
    {
        /// <summary>
        /// Constructor for Validator
        /// </summary>
        public TaskValidator()
        {
            RuleFor(x=>x.Title)
                .NotEmpty()
                .WithMessage("Заголовок задачи не может быть пустым").
                MaximumLength(128)
                .WithMessage("Заголовок задачи должен содержать не более 128 символов");
            RuleFor(x=>x.Description)
                .NotEmpty()
                .WithMessage("Описание задачи не может быть пустым")
                .MaximumLength(1024)
                .WithMessage("Описание задачи должен содержать не более 1024 символов");
            RuleFor(x=>x.DueTime)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Дата выполнения задачи должна быть в будущем");
            RuleFor(x=>x.CompletionTime)
                .GreaterThan(DateTime.UtcNow);
            RuleFor(x=>x.CreateTime).NotEmpty();
        }
    }
}
