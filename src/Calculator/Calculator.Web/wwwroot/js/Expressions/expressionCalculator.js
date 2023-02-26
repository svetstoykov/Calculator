function sendPostRequest() {
    const input = $('#expression-input');
    const expression = input.val();

    $.ajax({
        url: '/post-endpoint',
        method: 'POST',
        data: { expression },
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
    sendPostRequest();
  });