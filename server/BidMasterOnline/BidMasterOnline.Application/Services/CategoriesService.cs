using AutoMapper;
using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.RepositoryContracts;
using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Specifications;
using BidMasterOnline.Domain.Entities;

namespace BidMasterOnline.Application.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IAsyncRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesService(IAsyncRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateNewCategoryAsync(CreateCategoryDTO category)
        {
            // Validating category
            if (category is null)
                throw new ArgumentNullException("Category is null.");

            if (string.IsNullOrEmpty(category.Name))
                throw new ArgumentNullException("Name cannot be blank.");

            if (string.IsNullOrEmpty(category.Description))
                throw new ArgumentNullException("Description cannot be blank.");

            if (await _repository.AnyAsync<Category>(x => x.Name == category.Name && x.IsDeleted == false))
                throw new ArgumentException("Category with the same name already exists.");

            // Creating new category
            var categoryToCreate = this._mapper.Map<Category>(category);
            categoryToCreate.Id = Guid.NewGuid();

            await _repository.AddAsync(categoryToCreate);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            // Validating category
            var category = await _repository.GetByIdAsync<Category>(id);

            if (category is null)
                throw new KeyNotFoundException("Category with such id does not exist.");

            if (category.IsDeleted)
                throw new InvalidOperationException("Category has already been deleted.");

            // Deleting category
            category.IsDeleted = true;
            await _repository.UpdateAsync(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesListAsync(CategorySpecificationsDTO specifications)
        {
            var categories = await _repository.GetAsync<Category>(this.GetSpecification(specifications));

            return categories.Select(x => this._mapper.Map<CategoryDTO>(x)).ToList();
        }

        // Method for creating ISpecification for CategorySpecificationsDTO
        private ISpecification<Category> GetSpecification(CategorySpecificationsDTO specifications, bool isDeleted = false)
        {
            var builder = new SpecificationBuilder<Category>();

            builder.With(x => x.IsDeleted == isDeleted);

            if (specifications is not null)
            {
                if (!string.IsNullOrEmpty(specifications.Name))
                    builder.With(x => x.Name.Contains(specifications.Name));

                switch (specifications.SortField)
                {
                    case "Id":
                        builder.OrderBy(x => x.Id, specifications.SortOrder);
                        break;
                    case "Name":
                        builder.OrderBy(x => x.Name, specifications.SortOrder);
                        break;
                }

                builder.WithPagination(specifications.PageSize, specifications.PageNumber);
            }

            return builder.Build();
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync<Category>(id);

            if (category is null)
                throw new KeyNotFoundException("Category with such Id does not exist.");

            return this._mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetDeletedCategoriesListAsync(CategorySpecificationsDTO specifications)
        {
            var categories = await _repository.GetAsync<Category>(this.GetSpecification(specifications, isDeleted: true));

            return categories.Select(x => this._mapper.Map<CategoryDTO>(x)).ToList();
        }

        public async Task RecoverCategoryAsync(Guid id)
        {
            // Validating category
            var category = await _repository.GetByIdAsync<Category>(id);

            if (category is null)
                throw new KeyNotFoundException("Category with such id does not exist.");

            if (!category.IsDeleted)
                throw new InvalidOperationException("Category is not deleted to recover it.");

            if (await _repository.AnyAsync<Category>(x => x.Name == category.Name && x.IsDeleted == false))
                throw new InvalidOperationException("Cannot recover the category: another category with the same name alreay exists.");

            // Deleting category
            category.IsDeleted = false;
            await _repository.UpdateAsync(category);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO category)
        {
            // Validating category
            if (category is null)
                throw new ArgumentNullException("Category is null.");

            var existantCategory = await _repository.GetByIdAsync<Category>(category.Id);

            if (existantCategory is null)
                throw new KeyNotFoundException("Category with such id does not exist.");

            if (string.IsNullOrEmpty(category.Name))
                throw new ArgumentNullException("Name cannot be blank.");

            if (string.IsNullOrEmpty(category.Description))
                throw new ArgumentNullException("Description cannot be blank.");

            if (await _repository.AnyAsync<Category>(x => x.Id != category.Id &&
                                                          x.Name == category.Name && 
                                                          x.IsDeleted == false))
                throw new ArgumentException("Category with the same name already exists.");

            // Updating category
            existantCategory = this._mapper.Map<Category>(category);
            await _repository.UpdateAsync(existantCategory);
        }
    }
}
