using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Categories.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<CategoryResponse>>
    {

    }
}
