using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices.Images;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using ic_tienda_data.sources.BaseDeDatos.Models;
using ic_tienda_utils.Validations;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IcTiendaDbContext _context;
        private readonly IImageService _image;
        public CategoryRepository(IcTiendaDbContext context, IImageService imageFirebase)
        {
            _context = context;
            _image = imageFirebase;
        }

        public async Task<CategoryResponse> AddAsync(CategoryRequest categoryRequest)
        {
#pragma warning disable CS8604
            DataValidator.ValidateRequired(categoryRequest.Name, "nombre de la categoría");

            bool exists = await _context.Categories.AnyAsync(c => c.Name == categoryRequest.Name);
            DataValidator.ValidateUniqueName(exists, "nombre de la categoría");
            DataValidator.ValidateRequiredImage(categoryRequest.ImgPath, "imagen de la categoría");

#pragma warning restore CS8604
            var category = new Category
            {
                Name = categoryRequest.Name,
                Description = categoryRequest.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // if (categoryRequest.ImgPath != null && categoryRequest.ImgPath.Length > 0)
            //     category.ImgUrl = await _image.UploadImageAsync(categoryRequest.ImgPath, $"categoria_{category.Id}");

            category.ImgUrl = await _image.UploadImageAsync(categoryRequest.ImgPath, $"categoria_{category.Id}");

            await _context.SaveChangesAsync();
            return category.MapCategoryTo();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"No se encontró la categoría con ID {id}.");
            }

            if (category != null)
            {
                // Eliminar la imagen asociada si existe
                if (!string.IsNullOrEmpty(category.ImgUrl))
                {
                    var fileName = _image.GetPublicIdFromUrl(category.ImgUrl);
                    Console.WriteLine($"Intentando eliminar imagen con PublicId: {fileName}");
                    await _image.DeleteImageAsync(fileName);
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResponse<CategoryResponse>> GetAllAsync(QueryObject query)
        {
            var queryable = _context.Categories.AsQueryable();

            // Filtra los elementos si 'Search' tiene un valor.
            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(x => EF.Functions.Like(EF.Property<string>(x, "Name"), $"%{query.Search}%"));
            }

            // Aplica ordenación si 'SortBy' tiene un valor.
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                queryable = query.IsDecsending
                ? queryable.OrderByDescending(e => e.Id)
                : queryable.OrderBy(e => e.Id);
            }

            // Realiza la paginación.
            var totalCount = await queryable.CountAsync();
            var items = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            // Usamos el método MapCategoriesTo para mapear los items
            var mappedItems = items.MapCategoriesTo();

            return new PaginatedResponse<CategoryResponse>
            {
                Items = mappedItems,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return category.MapCategoryTo();
        }

        public async Task<CategoryResponse> UpdateAsync(int id, CategoryRequest categoryRequest)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            // Verificar si hay cambios en el nombre, descripción o imagen
            var hasChanges = category.Name != categoryRequest.Name ||
                            category.Description != categoryRequest.Description ||
                            (categoryRequest.ImgPath != null && categoryRequest.ImgPath.Length > 0);

            // Verificar si hay cambios en el nombre, descripción o imagen
            if (!hasChanges) throw new InvalidOperationException("No cambios detectados.");


            // Verificar si el nuevo nombre ya existe en otra categoría
            //var nameExists = await _context.Categories
            //   .AnyAsync(c => c.Name == categoryRequest.Name && c.Id != id);

            bool exists = await _context.Categories.AnyAsync(c => c.Name == categoryRequest.Name);
            DataValidator.ValidateUniqueName(exists, "nombre de la categoría");


            // Actualiza el nombre y descripción
            category.Name = categoryRequest.Name;
            category.Description = categoryRequest.Description;

            // Manejar la imagen solo si se proporciona una nueva
            if (categoryRequest.ImgPath != null && categoryRequest.ImgPath.Length > 0)
            {
                // Eliminar la imagen anterior si existe
                if (!string.IsNullOrEmpty(category.ImgUrl))
                {
                    var fileName = _image.GetPublicIdFromUrl(category.ImgUrl);

                    await _image.DeleteImageAsync(fileName);
                }

                // Subir la nueva imagen a Cloudinary usando ImageService
                var imageUrl = await _image.UploadImageAsync(categoryRequest.ImgPath, $"categoria_{category.Id}");
                category.ImgUrl = imageUrl;
            }

            await _context.SaveChangesAsync();

            return category.MapCategoryTo();
        }
    }
}