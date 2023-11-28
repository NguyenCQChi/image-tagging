export const ADMIN_USERNAME = 'admin';
export const ADMIN_PASSWORD = '111';
export const USER_TEST = 'john';
export const USER_PASSWORD = '123';

export const API_AUTH_SERVER = "https://bcit-backend.miniaturepug.info"
// export const API_AUTH_SERVER = "http://127.0.0.1:8000"
export const API_AUTH_REGISTER = "/api/v1/auth/register"
export const API_AUTH_LOGIN = "/api/v1/auth/login"
export const API_AUTH_VALIDATE = "/api/v1/auth/validate"
export const API_AUTH_REFRESH = "/api/v1/auth/refresh"
export const API_AUTH_REVOKE = "/api/v1/auth/revoke"
export const API_AUTH_RESETPASSWORD = "/api/v1/auth/resetPassword"
export const API_AUTH_GET_USERS = "api/v1/auth/allUserInformation"
export const API_AUTH_ACCESS_TOKEN = "accessToken"
export const API_AUTH_REFRESH_TOKEN = "refreshToken"

export const API_IMAGE_SERVER = "https://simarcodes.com/COMP4537/projects"
export const API_IMAGE_GET_CAPTION = "/get-caption"
export const API_IMAGE_GET_CAPTION_URL_PARAM = "imageUrl"

export const ROLE_USER = "user"
export const ROLE_ADMIN = "admin"

export const users = {
  "statusCode": 200,
    "isSuccess": true,
    "errorMessages": [],
    "result": [
        {
            "id": "b4f56713-708f-44e5-800f-7e10d8b7f13a",
            "userName": "simarv07",
            "name": "Simar",
            "email": "simarv07@gmail.com",
            "endpointInfo": {
                "POST/api/v1/auth/validate": 24,
                "POST/api/v1/auth/register": 1,
                "POST/api/v1/auth/login": 7
            },
            "refreshToken": "48891937-49c1-4887-b374-996e9cb854d0-7741ec25-b7df-45cb-9b6c-d504f38f875d",
            "expiresAt": "2023-12-05T02:00:44.373601"
        },
        {
            "id": "d735c694-0157-497a-8fa3-97379db3327b",
            "userName": "admin",
            "name": "Administrator2",
            "email": "msrandhawa9957@gmail.com",
            "endpointInfo": {
                "GET/api/v1/auth/allUserInformation": 2,
                "POST/api/v1/auth/login": 4,
                "GET/api/v1/auth/getAllEndpoints": 2,
                "GET/api/v1/auth/totalRequestsPerEndpoint": 1
            },
            "refreshToken": "9e144105-d8e8-43c7-90c1-e9e3f2e5e7f1-4fc47fca-ddee-41b2-8016-1b655e9f7bf1",
            "expiresAt": "2023-12-05T02:26:20.570442"
        }
    ]
}
