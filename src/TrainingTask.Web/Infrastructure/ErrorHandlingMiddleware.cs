using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingTask.Common.Contract;
using TrainingTask.Common.Errors;
using TrainingTask.Common.Exceptions;

namespace TrainingTask.Web.Infrastructure
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (!httpContext.Response.HasStarted && httpContext.Response.StatusCode >= 400)
                {
                    var responseModel = new ResponseModelBase { Message = GetErrorMessageByCode(httpContext.Response.StatusCode), Errors = new List<ErrorInfo>()};

                    var result = new JsonResult(responseModel);
                    await httpContext.ExecuteResultAsync(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during processing request");

                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                var (statusCode, message) = GetErrorDetailsByExceptionType(ex);
                var responseModel = new ResponseModelBase { Message = message, Errors = new List<ErrorInfo>()};

                if (ex is LogicException appException  && appException.Errors != null && appException.Errors.Count > 0)
                {
                    responseModel.Errors = appException.Errors;
                    foreach (var error in responseModel.Errors)
                    {
                        if (!string.IsNullOrEmpty(error.Message) || string.IsNullOrEmpty(error.Key))
                        {
                            continue;
                        }

                        error.Message = error.Key;
                        error.Key = null;
                    }
                }

                httpContext.Response.StatusCode = statusCode;

                var result = new JsonResult(responseModel);
                await httpContext.ExecuteResultAsync(result);
            }
        }

        private Tuple<int, string> GetErrorDetailsByExceptionType(Exception exception)
        {
            switch (exception)
            {
                case LogicException _:
                {
                    return new Tuple<int, string>(StatusCodes.Status400BadRequest, GetErrorMessageByCode(StatusCodes.Status400BadRequest));
                }

                case ObjectNotFoundException _:
                {
                    return new Tuple<int, string>(StatusCodes.Status404NotFound, GetErrorMessageByCode(StatusCodes.Status404NotFound));
                }

                case NotAuthorizedException _:
                {
                    return new Tuple<int, string>(StatusCodes.Status401Unauthorized, GetErrorMessageByCode(StatusCodes.Status401Unauthorized));
                }

                case SecurityException _:
                {
                    return new Tuple<int, string>(StatusCodes.Status403Forbidden, GetErrorMessageByCode(StatusCodes.Status403Forbidden));
                }

                case ConcurrencyException _:
                {
                    return new Tuple<int, string>(StatusCodes.Status409Conflict, GetErrorMessageByCode(StatusCodes.Status409Conflict));
                }

                default:
                {
                    return new Tuple<int, string>(StatusCodes.Status500InternalServerError, GetErrorMessageByCode(StatusCodes.Status500InternalServerError));
                }
            }
        }

        private string GetErrorMessageByCode(int code)
        {
            switch (code)
            {
                case StatusCodes.Status400BadRequest:
                {
                    return "Validation error ";
                }

                case StatusCodes.Status404NotFound:
                {
                    return "Not found error";
                }

                case StatusCodes.Status401Unauthorized:
                {
                    return "Not authorized";
                }

                case StatusCodes.Status403Forbidden:
                {
                    return "Security error(forbiten)";
                }

                case StatusCodes.Status409Conflict:
                {
                    return "Concurency error";
                }

                default:
                {
                    return "Unexpected error";
                }
            }
        }
    }
}
