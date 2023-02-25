using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Web.Common.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BaseApiController : Controller
{
   protected IMediator Mediator;

   public BaseApiController(IMediator mediator)
   {
      Mediator = mediator;
   }
}