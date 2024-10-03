import axios from 'axios';

// export const loginService = ({ email, password }) => {
//     return axios.post('/Login/login', {
//         email,`
//         password,
//     });
// };

export const loginService = async ({ email, password }) => {
    try {
      const response = await axios.post('http://localhost:5250/api/Login/login', {
        email: email,  // thay "email" bằng thông tin email thực tế
        password: password  // thay "password" bằng thông tin mật khẩu thực tế
      });
      return response.data; // trả về dữ liệu phản hồi từ API
    } catch (error) {
      console.error('Error during login:', error);
      throw error; // ném lỗi để xử lý ở phần khác (nếu cần)
    }
  };

export const signUpService = ({ email, password, name, age, school }) => {
    return axios.post('/auth/signup', {
        email,
        password,
        name,
        age,
        school,
    });
};

export const logoutService = () => {
    return axios.post('/auth/logout');
};
