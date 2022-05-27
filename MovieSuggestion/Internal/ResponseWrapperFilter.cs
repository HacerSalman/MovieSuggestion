using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieSuggestion.Core.Models;
using MovieSuggestion.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSuggestion.Api.Internal
{
    public class ResponseWrapperFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        private string SerializeException(Exception exp)
        {
            string msg = exp.Message;
            if (exp.InnerException != null)
            {
                msg += " " + exp.InnerException.Message;
                if (exp.InnerException.InnerException != null)
                {
                    msg += " " + exp.InnerException.InnerException.Message;
                }
            }
            return msg;
        }



        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is null)
            {

                if (context.Result.GetType() == typeof(OkObjectResult))
                {
                    if (((OkObjectResult)context.Result).Value != null &&
                        typeof(PagedResponse<>).Name == ((OkObjectResult)context.Result).Value.GetType().Name)
                    {
                        dynamic res = ((OkObjectResult)context.Result).Value;
                        context.Result = new OkObjectResult(new BaseResponse()
                        {
                            Data = res.Result,
                            Error = null,
                            Paging = res.Paging
                        });
                    }
                    else
                    {
                        object current = ((OkObjectResult)context.Result).Value;
                        context.Result = new OkObjectResult(new BaseResponse()
                        {
                            Data = current,
                            Error = null,
                            Paging = null
                        });
                    }
                }
                else if (context.Result.GetType() == typeof(ObjectResult))
                {
                    if (((ObjectResult)context.Result).Value != null &&
                        typeof(PagedResponse<>).Name == ((ObjectResult)context.Result).Value.GetType().Name)
                    {
                        dynamic res = ((ObjectResult)context.Result).Value;
                        context.Result = new OkObjectResult(new BaseResponse()
                        {
                            Data = res.Result,
                            Error = null,
                            Paging = res.Paging
                        });
                    }
                    else
                    {
                        object current = ((ObjectResult)context.Result).Value;
                        context.Result = new OkObjectResult(new BaseResponse()
                        {
                            Data = current,
                            Error = null,
                            Paging = null
                        });
                    }
                }
                else if (context.Result.GetType() == typeof(BadRequestObjectResult))
                {
                    context.Result = new BadRequestObjectResult(new BaseResponse()
                    {
                        Data = null,
                        Error = new Error()
                        {
                            Code = 400,
                            Message = ((BadRequestObjectResult)context.Result).Value.ToString()
                        },
                        Paging = null
                    });
                }
                else
                {
                    context.Result = new ConflictObjectResult(new BaseResponse()
                    {
                        Data = null,
                        Error = new Error()
                        {
                            Code = 409,
                            Message = Resource.BAD_CODE_DETECTED
                        },
                        Paging = null
                    });
                }
            }
            else
            {
                if (context.Exception is AggregateException ae)
                {
                    var msg = "";
                    foreach (var ie in ae.InnerExceptions)
                    {
                        msg += SerializeException(ie) + " ";
                    }
                    context.Result = new BadRequestObjectResult(new BaseResponse()
                    {
                        Data = null,
                        Error = new Error()
                        {
                            Code = 400,
                            Message = msg
                        },
                        Paging = null
                    });
                    context.ExceptionHandled = true;
                }
                else if (context.Exception != null)
                {
                    context.Result = new BadRequestObjectResult(new BaseResponse()
                    {
                        Data = null,
                        Error = new Error()
                        {
                            Code = 400,
                            Message = SerializeException(context.Exception)
                        },
                        Paging = null
                    });

                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
