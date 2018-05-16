using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine("AuthorizationAsync Filter Executed");

        var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        int num = 0;

        if(controllerActionDescriptor != null)
        {
            //var isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true)
            //    .Any(x => x.GetType().Equals(typeof(ChenFilterAttribute)));
            //Console.WriteLine("isDefined Count " + controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(ChenFilterAttribute), false).Count());
            context.RouteData.DataTokens.Add("User", "Hello world");
            if(num == 0)
                context.HttpContext.Items["num"] = num;
            context.HttpContext.Items["num2"] = num;
            num++;
        }
        Console.WriteLine("Items Count is " + context.HttpContext.Items.Count);
        //context.Result = new BadRequestObjectResult("Bad Request!");
        
    }
}