﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DataPersistency.Filters
{
    public class InternalServerErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Check if it's an unhandled exception (not a specific type you want to handle differently)
            if (context.ExceptionHandled == false)
            {
                context.ExceptionHandled = true; // Mark the exception as handled
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
