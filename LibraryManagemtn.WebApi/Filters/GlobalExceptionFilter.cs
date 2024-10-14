using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            HttpResponse response = context.HttpContext.Response;

            _logger.LogError(exception, $"An exception occurred: {exception.Message}");

            switch (exception)
            {
                case KeyNotFoundException:
                    context.Result = CreateNotFoundResponse(exception.Message);
                    response.StatusCode = 404;
                    break;
                case SqlException sqlException:
                    context.Result = CreateDatabaseErrorResponse(sqlException);
                    response.StatusCode = 500;
                    break;
                case InvalidOperationException:
                    context.Result = CreateBadRequestResponse(exception.Message);
                    response.StatusCode = 400;
                    break;
                default:
                    context.Result = CreateInternalServerErrorResponse(exception.Message);
                    response.StatusCode = 500;
                    break;
            }
        }

        private IActionResult CreateNotFoundResponse(string message)
        {
            return new NotFoundObjectResult(new { StatusCode = 404, Message = message, Details = "The requested resource was not found." });
        }

        private IActionResult CreateDatabaseErrorResponse(SqlException sqlException)
        {
            return new ObjectResult(new { StatusCode = 500, Message = "A database error occurred.", Details = GetSqlExceptionMessage(sqlException) });
        }

        private IActionResult CreateBadRequestResponse(string message)
        {
            return new BadRequestObjectResult(new { StatusCode = 400, Message = message });
        }

        private IActionResult CreateInternalServerErrorResponse(string message)
        {
            return new ObjectResult(new { StatusCode = 500, Message = "An unexpected error occurred.", Details = message });
        }

        private string GetSqlExceptionMessage(SqlException sqlException)
        {
            switch (sqlException.Number)
            {
                case -2:
                    return "The request to the database timed out.";
                case 53:
                case 233:
                    return "Unable to connect to the database.";
                case 4060:
                    return "The database is unavailable.";
                case 1205:
                    return "A database deadlock occurred.";
                case 701:
                    return "The database server is out of memory.";
                default:
                    return "An unexpected database error occurred.";
            }
        }
    }
}
