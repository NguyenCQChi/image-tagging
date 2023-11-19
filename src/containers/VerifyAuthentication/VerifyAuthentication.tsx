import { useRouter } from 'next/router';
import { useEffect } from 'react';

const VerifyAuthentication = () => {
    useEffect(() => {
        console.log("Checking User Login");
        // Add code to make a request to the /verify endpoint to check cookies

    }, []);
    return null;
}

export default VerifyAuthentication;
