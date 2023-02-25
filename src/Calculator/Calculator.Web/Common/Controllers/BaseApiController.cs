using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Web.Common.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseApiController : ControllerBase
{
   protected IMediator Mediator;

   public BaseApiController(IMediator mediator)
   {
      Mediator = mediator;
   }
}