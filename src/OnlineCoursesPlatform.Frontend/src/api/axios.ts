import axios, { AxiosError, type AxiosRequestConfig } from "axios";
import {
  getAccessToken,
  getRefreshToken,
  saveTokens,
  clearTokens,
} from "../services/tokenService"; // путь зависит от структуры проекта

interface RefreshResponse {
  accessToken: string;
  refreshToken?: string;
}

const axiosInstance = axios.create({
  baseURL: "https://localhost:7025/api",
  headers: {
    "Content-Type": "application/json",
  },
});

// Добавляем access token ко всем запросам
axiosInstance.interceptors.request.use((config) => {
  const token = getAccessToken();
  if (token) {
    config.headers = config.headers || {};
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Обработка 401 и автоматическое обновление access токена
axiosInstance.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const originalRequest = error.config as AxiosRequestConfig & { _retry?: boolean };

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const refreshToken = getRefreshToken();
        if (!refreshToken) throw new Error("Refresh token not found");

        const response = await axios.post<RefreshResponse>(
          "https://localhost:5173/api/auth/refresh",
          { refreshToken }
        );

        const { accessToken, refreshToken: newRefreshToken } = response.data;
        saveTokens(accessToken, newRefreshToken);

        originalRequest.headers = originalRequest.headers || {};
        originalRequest.headers.Authorization = `Bearer ${accessToken}`;

        return axiosInstance(originalRequest);
      } catch (refreshError) {
        clearTokens();
        window.location.href = "/login";
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export default axiosInstance;
