using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GymManagement.PL.Common
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var actionName = context.ActionDescriptor.RouteValues["action"];

                var model = context.ActionArguments
                    .Values
                    .FirstOrDefault(v => v != null && !v.GetType().IsPrimitive && v.GetType() != typeof(string));

                context.Result = new ViewResult
                {
                    ViewName = actionName,
                    ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(
                        new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                        context.ModelState)
                    {
                        Model = model
                    }
                };
            }
        }
    }
}
