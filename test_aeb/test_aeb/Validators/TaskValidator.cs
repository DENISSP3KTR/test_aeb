using FluentValidation;
using test_aeb.Models;

namespace test_aeb.Validators
{
    public class TaskValidator : AbstractValidator<ToDo_model>
    {
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
            RuleFor(x=>x.Due_Time)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Дата выполнения задачи должна быть в будущем");
            RuleFor(x=>x.Completion_Time)
                .GreaterThan(DateTime.UtcNow);
            RuleFor(x=>x.Create_Time).NotEmpty();
        }
    }
}
