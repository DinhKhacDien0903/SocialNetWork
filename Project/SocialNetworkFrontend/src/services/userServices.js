import axios from '~/utils/axios';

export const getPersonalInfoService = async () => {
    try {
       return await axios.get('/user/getInfor');
    } catch (e) {
        console.log(e);
    }
    return;
};
