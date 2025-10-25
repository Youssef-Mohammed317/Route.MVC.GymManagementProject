using GymManagement.BLL.ViewModels.Category;
using GymManagement.BLL.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface ICategoryService
    {
        ViewResponse<IEnumerable<CategoryViewModel>> GetAllCategories();
    }
}
