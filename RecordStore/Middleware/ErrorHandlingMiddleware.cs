using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace RecordStore.Api.Middleware
{
	public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex) when (ex.InnerException is SqlException e && e.Number == 2601)
            {
                var errorMessage = e.Message;
                var entityMatch = Regex.Match(errorMessage, @"(?:'dbo\.)(\w+)(?:s')");
                var entity = entityMatch.Groups[1].Value;
                var valuesMatch = Regex.Match(errorMessage, @"\(.+\)");
                var values = valuesMatch.Groups[0].Value;
                var details = $"{entity} with values {values} already exists in the database.";

                var response = new
                {
                    message = $"Unable to add {entity} to the database.",
                    detail = details
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(response);
                context.Response.StatusCode = 422;
            }
        }
    }
}
