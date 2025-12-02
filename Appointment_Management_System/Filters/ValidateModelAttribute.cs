using Appointment_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Appointment_Management_System.Filters
{
    
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public class ResponseWrapper : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            if (context.Result is ObjectResult obj)
            {
                string controller = context.Controller.GetType().Name.Replace("Controller", "");
                int statusCode = obj.StatusCode ?? 200;
                string APIMessage;
                if (statusCode >= 200 && statusCode < 300)
                {
                    APIMessage = (context.HttpContext.Request.Method) switch
                    {
                        "POST" => $"{controller} created successfully",
                        "PUT" => $"{controller}  updated successfully",
                        "PATCH" => $"{controller}  updated successfully",
                        "DELETE" => $"{controller}  deleted successfully",
                        "GET" => $"{controller}  fetched successfully",
                        _ => $"{controller}'s Request successful"
                    };
                }
                else
                {
                    APIMessage = obj.Value?.ToString() ?? "No message provided";
                }


                var apiResponse = new CommonResponse
                {
                    Success = statusCode >= 200 && statusCode < 300,
                    StatusCode = statusCode,
                    Message = APIMessage,
                    Data = obj.Value,
                    Path = context.HttpContext.Request.Path
                };

                context.Result = new JsonResult(apiResponse)
                {
                    StatusCode = apiResponse.StatusCode
                };
            }
            base.OnActionExecuted(context);
        }
    }
    
}
