using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface IPlanService
    {
        ViewResponse<IEnumerable<PlanViewModel>> GetAllPlans();
        ViewResponse<PlanViewModel> GetPlanById(int id);
        ViewResponse<UpdatePlanViewModel> GetPlanByIdForUpdate(int id);
        ViewResponse<PlanViewModel> UpdatePlan(int id, UpdatePlanViewModel updateModel);
        ViewResponse<PlanViewModel> TogglePlan(int id);
    }
}
