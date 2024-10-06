import axios from 'axios';

export const loginService = async ({ email, password }) => {
    try {
        const response = await axios.post('/Login/login', {
            email: email,
            password: password,
        });

        return response.data;
    } catch (error) {
        console.error('Error during login:', error);
        throw error;
    }
};

export const signUpService = ({ email, password, firstName, lastName }) => {
    return axios.post('/Login/singup', {
        userName: lastName,
        password: password,
        email: email,
        firstName,
        lastName,
    });
};

export const logoutService = () => {
    return axios.post('/auth/logout');
};
