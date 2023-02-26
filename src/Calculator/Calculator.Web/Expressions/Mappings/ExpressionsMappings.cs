using AutoMapper;
using Calculator.Application.Expressions.Models;
using Calculator.Web.Expressions.Models.Response.History;

namespace Calculator.Web.Expressions.Mappings;

public class ExpressionsMappings : Profile
{
    public ExpressionsMappings()
    {
        CreateMap<ExpressionHistoryModel, SolvedExpressionViewModel>();
    }
}