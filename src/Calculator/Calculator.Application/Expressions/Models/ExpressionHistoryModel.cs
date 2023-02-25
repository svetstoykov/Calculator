namespace Calculator.Application.Expressions.Models;

public class ExpressionHistoryModel
{
    public string Expression { get; set; } = null!;

    public double Result { get; set; }
    
    public DateTime DateCreated { get; set; }
}