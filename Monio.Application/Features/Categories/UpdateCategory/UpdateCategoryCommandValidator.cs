using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Categories.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty()
                .WithMessage("Category ID không được để trống.");

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
