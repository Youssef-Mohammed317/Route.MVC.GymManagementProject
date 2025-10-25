using AutoMapper;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Category;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public ViewResponse<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var categories = unitOfWork.CategoryRepository.GetAll();

            return ViewResponse<IEnumerable<CategoryViewModel>>.Success(
                mapper.Map<IEnumerable<CategoryViewModel>>(categories)
            );
        }
    }
}
