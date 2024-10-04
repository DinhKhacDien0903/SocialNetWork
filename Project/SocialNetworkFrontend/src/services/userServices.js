import axios from '~/utils/axios';

export const getPersonalInfoService = async () => {
    try {
        var res = await axios.get('/user/getUsers');
    } catch (e) {
        console.log(e);
    }
    return;
};
