namespace Calculator.Web.Expressions.Models.Response.History;

public class EvaluatedExpressionHistoryViewModel
{
    public string Expression { get; set; } = null!;

    public double Result { get; set; }
    
    public DateTime DateCreated { get; set; }
}