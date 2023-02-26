namespace Calculator.Web.Expressions.Models.Response.History;

public class ExpressionHistoryViewModel
{
    public IEnumerable<SolvedExpressionViewModel> SolvedExpressions { get; set; }
        = new List<SolvedExpressionViewModel>();
}