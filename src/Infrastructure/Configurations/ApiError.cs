using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Infrastructure.Configurations
{
    public class ApiError
    {
        public string Message { get; set; }

        public ApiError() { }

        public ApiError(ModelStateDictionary modelState)
        {
            var errors = modelState?.Keys.SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage));
            Message = string.Join(Environment.NewLine, errors);
        }
    }
}