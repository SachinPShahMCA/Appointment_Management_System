using Appointment_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Appointment_Management_System.Filters
{
    
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Errors = x.Value.Errors.Select(e => e.ErrorMessage)
            });
            var response = new CommonResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                Message = "Validation failed",
                Data = errors,
                Path = context.HttpContext.Request.Path,
                CorrelationId = context.HttpContext.Response.Headers["X-Correlation-Id"]
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }

    public class ResponseWrapper : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            if (context.Result is not ObjectResult obj)
                return;

            var statusCode = obj.StatusCode ?? StatusCodes.Status200OK;

            if (statusCode < 200 || statusCode >= 300)
                return;

            string controller = context.Controller.GetType().Name.Replace("Controller", "");

            var message = context.HttpContext.Request.Method switch
            {
                "POST" => $"{controller} created successfully",
                "PUT" or "PATCH" => $"{controller} updated successfully",
                "DELETE" => $"{controller} deleted successfully",
                "GET" => $"{controller} fetched successfully",
                _ => "Request successful"
            };
            var apiResponse = new CommonResponse
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = obj.Value,
                Path = context.HttpContext.Request.Path,
                CorrelationId =
                context.HttpContext.Response.Headers["X-Correlation-Id"]
            };

            context.Result = new JsonResult(apiResponse)
            {
                StatusCode = apiResponse.StatusCode
            };
            base.OnActionExecuted(context);
        }
    }
    
}
