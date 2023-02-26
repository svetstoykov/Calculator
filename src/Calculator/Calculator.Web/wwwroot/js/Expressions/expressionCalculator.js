
let settings = {}

function configure(solveExpressionUrl) {
    settings.solveExpressionUrl = solveExpressionUrl
}

function solveExpression() {
    const input = $('#expression-input');
    const url = $('#expression-form').attr('url');
    const data = {
        Expression: input.val() 
    };

    $.ajax({
        url: url,
        method: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function(response) {
            console.log('Received data:', response);
            // Do something with the response data
        },
        error: function(xhr, status, error) {
            console.error('Error sending POST request:', error);
            // Handle the error
        }
    });
}

$('#expression-form').submit(function(event) {
    event.preventDefault();
    solveExpression();
  });