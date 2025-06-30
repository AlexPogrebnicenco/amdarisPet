import axios from "axios";
import { getAccessToken } from "../services/tokenService"; // оставить

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

export default axiosInstance;
