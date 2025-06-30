import { AxiosError, type AxiosRequestConfig } from "axios";
import axiosInstance from "./axios";
import {
  getRefreshToken,
  saveTokens,
} from "../services/tokenService";
import { useEffect } from "react";
import { useAuth } from "../context/AuthContext";

interface RefreshResponse {
  accessToken: string;
  refreshToken?: string;
}

export const useAxiosInterceptor = () => {
  const { logout } = useAuth();

  useEffect(() => {
    const responseInterceptor = axiosInstance.interceptors.response.use(
      (response) => response,
      async (error: AxiosError) => {
        const originalRequest = error.config as AxiosRequestConfig & { _retry?: boolean };

        if (error.response?.status === 401 && !originalRequest._retry) {
          originalRequest._retry = true;

          try {
            const refreshToken = getRefreshToken();
            if (!refreshToken) throw new Error("Refresh token not found");

            const response = await axiosInstance.post<RefreshResponse>(
              "/auth/refresh",
              { refreshToken }
            );

            const { accessToken, refreshToken: newRefreshToken } = response.data;
            saveTokens(accessToken, newRefreshToken);

            originalRequest.headers = originalRequest.headers || {};
            originalRequest.headers.Authorization = `Bearer ${accessToken}`;

            return axiosInstance(originalRequest);
          } catch (refreshError) {
            logout(); // ✅ Авто-logout через контекст
            return Promise.reject(refreshError);
          }
        }

        return Promise.reject(error);
      }
    );

    return () => {
      axiosInstance.interceptors.response.eject(responseInterceptor);
    };
  }, [logout]);
};
