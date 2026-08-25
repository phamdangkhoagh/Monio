using MediatR;
using Monio.Application.Features.Categories.GetCategories;
using Monio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Categories.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<CategoryResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public CategoryType Type { get; set; }
        public Guid? ParentId { get; set; }
    }
}
