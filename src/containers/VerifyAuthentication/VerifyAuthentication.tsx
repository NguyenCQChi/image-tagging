import { useRouter } from 'next/router';
import { useEffect } from 'react';
import { api } from '@src/utils/api';
import { decode } from "jsonwebtoken";
import {
    API_AUTH_ACCESS_TOKEN,
    API_AUTH_REFRESH_TOKEN,
    API_AUTH_SERVER,
    API_AUTH_VALIDATE,
    ROLE_USER,
    ROLE_ADMIN,
} from '@constants/strings';

const VerifyAuthentication = () => {
    const router = useRouter();
    useEffect(() => {
        console.log("Checking User Login");

        const accessToken = localStorage.getItem(API_AUTH_ACCESS_TOKEN);
        const refreshToken = localStorage.getItem(API_AUTH_REFRESH_TOKEN);

        if ( accessToken && refreshToken) {
            api.defaults.headers.common["Authorization"] = `Bearer ${accessToken}`;

            const post_body = {
                [API_AUTH_ACCESS_TOKEN]: accessToken,
                [API_AUTH_REFRESH_TOKEN]: refreshToken,
            }

            // Make a post request to validation endpoint
            const response = api.post(`${API_AUTH_SERVER}${API_AUTH_VALIDATE}`, post_body)
            response.then(
                () => {
                    // Route to regular page
                    console.log("Validated Successfully")

                    const decodedToken = decode(accessToken) as { [key: string]: any } | null;

                    const userRole = decodedToken.role

                    if (userRole == ROLE_USER) {
                        console.log("Routing to user page")
                        // router.push('/landing')
                    } else if (userRole == ROLE_ADMIN) {
                        console.log("Routing to admin page")
                        // router.push('/admin')
                    } else {
                        console.log("Undefined user role")
                    }
                },
                () => {
                    // Login and reset
                    console.log("Validation Failed")
                    // router.push('/')
                }
            )
        } else {
            // User needs to login since there are no JWT tokens.
            console.log("No jwt tokens found.")
            router.push('/')
        }
    }, []);
    return null;
}

export default VerifyAuthentication;
