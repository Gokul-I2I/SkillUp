document.getElementById('Email').addEventListener('blur', function () {
    var email = this.value;
    var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    var errorMessage = document.getElementById('emailError');

    // Check if the email matches the pattern
    if (!emailPattern.test(email)) {
        // If invalid, show the error message and style the input as invalid
        errorMessage.style.display = 'inline';
        this.style.borderColor = 'red';
    } else {
        // If valid, hide the error message and style the input as valid
        errorMessage.style.display = 'none';
        this.style.borderColor = 'green';
    }
});