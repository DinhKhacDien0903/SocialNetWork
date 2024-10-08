import axios from '~/utils/axios';

export const loginService = ({ username, password }) => {
    return axios.post('/auth/login', {
        username,
        password,
    });
};

export const signupService = ({ username, password, name, age, school }) => {
    return axios.post('/auth/signup', {
        username,
        password,
        name,
        age,
        school,
    });
};
