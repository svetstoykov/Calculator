function solveExpression() {
    const input = $('#expression-input');
    const url = $('#expression-form').attr('url');
    const historyList = $('#expression-history-list');
    const data = {
        Expression: input.val()
    };

    $.ajax({
        url: url,
        method: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response) {
            historyList.prepend
            (`<li>
                    ${data.Expression}
                </li>
                <li>
                   = ${response.data}
                </li>`)
        },
        error: function (response) {
            alert(response.responseJSON.message);
        },
        complete: function () {
            input.val('')
        }
    });
}

$('#expression-form').submit(function (event) {
    event.preventDefault();
    solveExpression();
});