async function submitForm() {
    // Get form elements
    var email = document.getElementById('email').value;
    var resetToken = document.getElementById('resetToken').value;
    var password = document.getElementById('password').value;
    var confirmPassword = document.getElementById('confirmPassword').value;

    // Validate email field
    if (email.trim() === '') {
        showMessage('error', 'Email field must be filled');
        return;
    }

    // Validate other fields
    if (resetToken.trim() === '' || password.trim() === '' || confirmPassword.trim() === '') {
        showMessage('error', 'All fields must be filled');
        return;
    }

    // Validate password match
    if (password !== confirmPassword) {
        showMessage('error', 'Passwords do not match');
        return;
    }

    // Create request body
    var requestBody = {
        password: password,
        confirmPassword: confirmPassword,
        token: resetToken
    };
    const url = `https://bcit-backend.miniaturepug.info/api/v1/auth/resetPassword?email=${encodeURIComponent(email)}`;
    const response = await fetch(url, {
        method: "PATCH",
        headers: {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin": "https://bcit.miniaturepug.info*"
        },
        body: JSON.stringify(requestBody)
    })
    data = await response.json()
    
    if (!response.ok) {
        if (response.status == 400) {
            const errorList = [];
            for (const propertyName in data.errors) {
                if (data.errors.hasOwnProperty(propertyName)) {
                    const errorMessages = data.errors[propertyName];
                    const description = errorMessages[0]; // Assuming there is only one description per property
                    errorList.push(`${propertyName}: ${description}`);
                }
            }
            showMessage('error', errorList.join('\n'));
        } else {
            showMessage('error', data.errorMessages.join('\n'));
        }
    } else {
        showMessage('success', data.result);
    }

    // // Make PATCH request
    // fetch('http://localhost:5062/api/v1/auth/resetPassword/' + email, {
    //     method: 'PATCH',
    //     headers: {
    //         'Content-Type': 'application/json',
    //     },
    //     body: JSON.stringify(requestBody)
    // })
    // .then(response => {
    //     if (response.ok) {
    //         return response.json();
    //     } else {
    //         throw response;
    //     }
    // })
    // .then(data => {
    //     // Handle success response
    //     showMessage('success', data.result);
    // })
    // .catch(error => {
    //     // Handle error
    //     if (error.status === 400) {
    //         error.json().then(problemDetails => {
    //             // Display the specific error details from the server
    //             showMessage('error', problemDetails.detail);
    //         });
    //     } else {
    //         showMessage('error', 'An error occurred. Please try again later.');
    //     }
    // });
}

function showMessage(type, text) {
    var messageDiv = document.getElementById('message');
    messageDiv.textContent = text;
    messageDiv.className = 'message ' + type;

    // Clear message after 5 seconds
    setTimeout(function() {
        messageDiv.textContent = '';
        messageDiv.className = 'message';
    }, 60000);
}
