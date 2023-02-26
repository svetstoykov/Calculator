using AutoMapper;
using Calculator.Application.Common.Result.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Web.Common.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BaseApiController : Controller
{
   protected readonly IMediator Mediator;
   protected readonly IMapper Mapper;

   public BaseApiController(IMediator mediator, IMapper mapper)
   {
      Mediator = mediator;
      Mapper = mapper;
   }
   
   protected ActionResult HandleResult<TOutputData>(Result<TOutputData> result) 
      => result.IsSuccessful ? Ok(result) : BadRequest(result);
}