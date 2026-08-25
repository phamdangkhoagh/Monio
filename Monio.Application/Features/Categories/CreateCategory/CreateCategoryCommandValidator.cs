using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .WithMessage("Tên danh mục không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên danh mục không được vượt quá 100 ký tự.");

            RuleFor(q => q.Type)
                .IsInEnum()
                .WithMessage("Loại danh mục không hợp lệ.");

            RuleFor(q => q.Icon)
                .MaximumLength(100)
                .When(q => q.Icon != null);

            RuleFor(q => q.Color)
                .MaximumLength(20)
                .When(q => q.Color != null);
        }
    }
}
