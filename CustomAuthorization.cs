using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using testAPI.Models;

public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
{
    public string Roles { get; set; }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
        Console.WriteLine(configuration.GetConnectionString("dbChen"));
        Console.WriteLine("AuthorizationAsync Filter Executed");

        var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        int num = 0;

        if (controllerActionDescriptor != null)
        {
            //var isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true)
            //    .Any(x => x.GetType().Equals(typeof(ChenFilterAttribute)));
            //Console.WriteLine("isDefined Count " + controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(ChenFilterAttribute), false).Count());
            context.RouteData.DataTokens.Add("User", "Hello world");
            if (num == 0)
                context.HttpContext.Items["num"] = num;
            context.HttpContext.Items["num2"] = num;
            num++;
            Position position = new Position();
            position.Latitude = "123";
            position.Longitude = "456";
            context.HttpContext.Items["Position"] = position;

            //Custom Claim
            var identity = (ClaimsIdentity)context.HttpContext.User.Identity;
            identity.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, "A Person"));
            identity.AddClaim(new Claim(ClaimTypes.Name, "Chen"));


        }
        Console.WriteLine("Items Count is " + context.HttpContext.Items.Count);
        //context.Result = new BadRequestObjectResult("Bad Request!");

    }
}