# Authentication Microservice Documentation

## Dependencies
- Docker (https://docs.docker.com/engine/install/)
- Docker Compose (https://docs.docker.com/compose/install/)

## Setup
- export SENDGRID_API_KEY=`<API_KEY>`
- export PASSWORD=`<ANY RANDOM PASSWORD CONTAINING LETTERS AND NUMBERS, NO SYMBOLS>`
- cd authentication
- docker compose up --build (Let it run, it will fail, and automatically restart itself a few times while the database is being initialized. When you see `authentication-api-1       | I am running`, the service is up.)
- Navigate to http://localhost:8000/api/v1/auth/doc/swagger/index.html and explore the swagger doc.


## Overview
- Expand each endpoint to see the list of status codes and request/response body structure.
- Authorization: `Bearer <Access Token>`. Expand /api/v1/auth/validate for more info.
- Whenever you get 400 Bad request, the response body has an object called errors.
- Whenever you get any other kind of 4xx or 5xx status code, the errors are in an array called errorMessages.
- Whenever you get a 2xx success, if a response is expected, it will be in the result object.

Example on how to handle different status codes can be found in `<project_root>/frontend/resetPassword/js/resetPassword.js`.

Code snippet for handling different responses:

```javascript
const response = await fetch('http://localhost:5062/api/v1/auth/resetPassword/' + email, {
    method: 'PATCH',
    headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
    },
    body: JSON.stringify(requestBody)
});

const data = await response.json();

if (!response.ok) {
    if (response.status === 400) {
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
```

## How to Use Different Endpoints
### Register
- Use the `/api/v1/auth/register` endpoint. On success, redirect to login.

### Login
- Use `/api/v1/auth/login`. On success, you will receive refreshToken and accessToken. Save these in your browser storage. Decode using HS256 algorithm (jwt.io) to find the username (unique_name) and role. Redirect based on the role.
- [JWT decodeing](https://jwt.io/). Open the link and decode:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXIxIiwicm9sZSI6InVzZXIiLCJqdGkiOiJKVEk4MmI2ZTVjYi1lNjNkLTRjY2QtOWExNC0xMWM5OWQyNGU0OGIiLCJzdWIiOiJiMjc3MDJkMS0yYThlLTRlZTctYTU0NS03MDlmMmQ1ZGNhYjMiLCJhdWQiOlsiYjI3NzAyZDEtMmE4ZS00ZWU3LWE1NDUtNzA5ZjJkNWRjYWIzIiwiKmxvY2FsaG9zdDoqIl0sIm5iZiI6MTcwMDI3MzQwMywiZXhwIjoxNzAwMjczNDYzLCJpYXQiOjE3MDAyNzM0MDMsImlzcyI6Iipsb2NhbGhvc3Q6KiJ9.q7cqpzbC85wgffIAe2RteHhRtLIz6Vhkx-AzvbXLqUU5c
```

### Authentication/Validation
- Before making API calls, authenticate/validate using `/api/v1/auth/validate`. Use Bearer authentication with the accessToken. 401 unauthorized errors mean that you need to refresh your tokens. 417 means that immediately log the user out.

### Refresh Token
- If unauthorized, send a request to `/api/v1/auth/refresh`. On success, receive a new AccessToken along with the old refreshToken. Handle 417 ExpectationFailed as a malicious user and log them out.

### Logout
- Make a request to `/api/v1/auth/revoke` to invalidate all user tokens. Remove tokens from browser storage. Ignore all status codes in the response.

### Password Reset
- To request a password reset, make a GET request to `/api/v1/auth/resetPassword/?email={EMAIL}`. If legit, the user will receive an email for password reset with a proper link. Frontend doesn't handle actual password reset.

### Get user Info
- To request user info, make a GET request to `/api/v1/auth/userInformation?userName={USERNAME}`. This is a privileged request and only admins can make this request. The user role i.e. admin is verified by the server as well. When making this request, make sure you authenticate properly.
- The GET request needs these 2 headers: `Authorization: "Bearer {ACCESS_TOKEN}"` and `X-Refresh-Token: "{REFRESH_TOKEN}"`

### Get All users Info
- To request user info, make a GET request to `/api/v1/auth/allUserInformation`. This is a privileged request and only admins can make this request. The user role i.e. admin is verified by the server as well. When making this request, make sure you authenticate properly.
- The GET request needs these 2 headers: `Authorization: "Bearer {ACCESS_TOKEN}"` and `X-Refresh-Token: "{REFRESH_TOKEN}"`

### Get Total Requests per endpoint
- To request the total number of requests per endpoint, make a GET request to `/api/v1/auth/totalRequestsPerEndpoint`. This is a privileged request and only admins can make this request. The user role i.e. admin is verified by the server as well. When making this request, make sure you authenticate properly.
- The GET request needs these 2 headers: `Authorization: "Bearer {ACCESS_TOKEN}"` and `X-Refresh-Token: "{REFRESH_TOKEN}"`

### Get names of all endpoints
- To request the list of all endpoints, make a GET request to `/api/v1/auth/getAllEndpoints`. This is a privileged request and only admins can make this request. The user role i.e. admin is verified by the server as well. When making this request, make sure you authenticate properly.
- The GET request needs these 2 headers: `Authorization: "Bearer {ACCESS_TOKEN}"` and `X-Refresh-Token: "{REFRESH_TOKEN}"`


### NOTE
Login and refresh return both accessToken and refreshToken. Along with that, they also return cookies embeeded into response headers which you can take advantage of. What this means is, either use response to manually get the tokens, or take advantage of cookies.
Cookie Names: `Authorization` and `X-Refresh-Token`
