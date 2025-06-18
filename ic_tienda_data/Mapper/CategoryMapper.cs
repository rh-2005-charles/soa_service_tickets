using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryResponse MapCategoryTo(this Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImgPath = category.ImgUrl
            };
        }

        // Puedes agregar más mapeos si necesitas
        public static List<CategoryResponse> MapCategoriesTo(this IEnumerable<Category> categories)
        {
            return categories.Select(category => category.MapCategoryTo()).ToList();
        }
    }
}