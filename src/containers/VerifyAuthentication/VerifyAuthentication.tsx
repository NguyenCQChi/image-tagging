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
} from '@src/constants/strings';

const VerifyAuthentication = () => {
    const router = useRouter();
    useEffect(() => {

        const accessToken = localStorage.getItem(API_AUTH_ACCESS_TOKEN);
        const refreshToken = localStorage.getItem(API_AUTH_REFRESH_TOKEN);

        if ( accessToken && refreshToken ) {
            console.log('accessToken: ' + accessToken)
            console.log('refreshToken: ' + refreshToken)
            api.defaults.headers.common["Authorization"] = `Bearer ${accessToken}`;
            api.defaults.headers.common["X-Refresh-Token"] = refreshToken;

            const post_body = {
                [API_AUTH_ACCESS_TOKEN]: accessToken,
                [API_AUTH_REFRESH_TOKEN]: refreshToken,
            }

            // Make a post request to validation endpoint
            const response = api.post(`${API_AUTH_SERVER}${API_AUTH_VALIDATE}`, post_body);
            response.then(
                () => {
                    console.log("Validate account successful");
                    // Route to regular page
                    const decodedToken = decode(accessToken) as { [key: string]: any } | null;

                    const userRole = decodedToken.role
                    if (userRole == ROLE_USER) {
                        router.push('/landing')
                    } else if (userRole == ROLE_ADMIN) {
                        router.push('/admin')
                    } else {
                        console.log("Undefined user role")
                    }
                },
                () => {
                    console.log("Validate account failed");
                    localStorage.removeItem(API_AUTH_REFRESH_TOKEN);
                    localStorage.removeItem(API_AUTH_ACCESS_TOKEN);
                    // Login and reset
                    router.push('/')
                }
            )
        } else {
            // User needs to login since there are no JWT tokens.
            router.push('/')
        }
    }, []);
    return null;
}

export default VerifyAuthentication;
