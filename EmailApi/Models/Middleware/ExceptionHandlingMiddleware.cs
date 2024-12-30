using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace EmailApi.Models.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            string message;
            if (exception is SmtpCommandException smtpEx && smtpEx.StatusCode == SmtpStatusCode.AuthenticationRequired)
            {
                message = "SMTP Authentication failed. Please check your email credentials.";
            }
            else if (exception.GetBaseException() is SqlException sqlEx)
            {
                if (sqlEx.Message.Contains("PK_User1"))
                    message = "User ID cannot be duplicate.";
                else if (sqlEx.Message.Contains("IX_User1_Email"))
                    message = "Email cannot be duplicate.";
                else if (sqlEx.Message.Contains("IX_User1_MobileNo"))
                    message = "Contact number cannot be duplicate.";
                else
                    message = "Database error occurred.";
            }
            else
            {
                message = "An unexpected error occurred.";
            }

            return httpContext.Response.WriteAsync(new
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}

