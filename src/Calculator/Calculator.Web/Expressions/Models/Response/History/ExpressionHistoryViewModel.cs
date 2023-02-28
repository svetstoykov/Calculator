namespace Calculator.Web.Expressions.Models.Response.History;

public class ExpressionHistoryViewModel
{
    public IEnumerable<EvaluatedExpressionHistoryViewModel> EvaluatedExpressionsHistory { get; set; }
        = new List<EvaluatedExpressionHistoryViewModel>();
}