namespace Calculator.Application.Expressions.Models;

public class EvaluatedExpressionHistoryModel
{
    public string Expression { get; set; } = null!;

    public double Result { get; set; }
    
    public DateTime DateCreated { get; set; }
}