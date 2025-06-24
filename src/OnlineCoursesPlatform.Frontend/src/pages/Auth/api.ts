// import axios from 'axios';

// const API_URL = 'https://localhost:7025/api/auth';  // заменить на свой URL

// interface AuthResultDto {
//   accessToken: string;
//   refreshToken: string;
//   accessTokenExpiration: string;
//   refreshTokenExpiration: string;
//   email: string;
//   userName: string;
// }

// export const login = async (data: {email: string; password: string}) => {
//     const response = await axios.post<AuthResultDto>(`${API_URL}/login`, data)
//     const auth = response.data;

//     localStorage.setItem('accessToken', auth.accessToken);
//     localStorage.setItem('refreshToken', auth.refreshToken);
//     localStorage.setItem('userName', auth.userName); // опционально
//     localStorage.setItem('email', auth.email); 
// };

// export const register = async (data: {
//   email: string;
//   password: string;
//   confirmPassword: string;
//   userName: string;
//   age: number;
//   gender: string;
// }) => {
//   const response = await axios.post<AuthResultDto>(`${API_URL}/register`, data);
//   const auth = response.data;

//   localStorage.setItem('accessToken', auth.accessToken);
//   localStorage.setItem('refreshToken', auth.refreshToken);
//   localStorage.setItem('userName', auth.userName);
//   localStorage.setItem('email', auth.email);
// };
