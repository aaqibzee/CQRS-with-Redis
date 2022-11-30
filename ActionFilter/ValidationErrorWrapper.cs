using System.Web.Http.ModelBinding;

namespace CQRS_with_Redis.ActionFilter
{
    internal class ValidationErrorWrapper
    {
        private ModelStateDictionary modelState;

        public ValidationErrorWrapper(ModelStateDictionary modelState)
        {
            this.modelState = modelState;
        }
    }
}