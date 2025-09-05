import { createContext, useContext, useEffect, useState } from "react";
import type { ReactNode } from "react";
import { jwtDecode } from "jwt-decode";
import { stopNotificationConnection } from "../services/notificationService";
import axiosInstance from "../api/axios";
import { saveTokens } from "../services/tokenService";

interface UserAccountInfoDto {
  userName: string;
  avatarUrl: string | null;
}

interface AuthContextType {
  userId: number | null;
  role: string | null;
  avatarUrl: string | null;
  userName: string | null;
  isAuthenticated: boolean;
  isAuthLoading: boolean;
  setAuthState: (
    token: string | null,
    userInfo?: UserAccountInfoDto | null
  ) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const extractUserId = (decodedJwt: any) => {
  return decodedJwt.sub ? Number(decodedJwt.sub) : null;
};

const extractUserRole = (decodedJwt: any) => {
  return (
    decodedJwt[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ] || null
  );
};
// const extractUserName = (decodedJwt: any) => {
//   return decodedJwt["username"] || null;
// };

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [userId, setUserId] = useState<number | null>(null);
  const [role, setRole] = useState<string | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [isAuthLoading, setIsAuthLoading] = useState(true);
  const [avatarUrl, setAvatarUrl] = useState<string | null>(null);
  const [userName, setUserName] = useState<string | null>(null);

  // Инициализация при загрузке страницы
  useEffect(() => {
    const token = localStorage.getItem("accessToken");

    const initializeAuth = async () => {
      if (token) {
        try {
          const decoded: any = jwtDecode(token);
          const currentTime = Date.now() / 1000;

          if (decoded.exp && decoded.exp > currentTime) {
            setUserId(extractUserId(decoded));
            setRole(extractUserRole(decoded));
            setIsAuthenticated(true);

            const userInfoString = localStorage.getItem("userInfo");
            if (userInfoString) {
              try {
                const userInfo = JSON.parse(userInfoString);
                setUserName(userInfo.userName);
                setAvatarUrl(userInfo.avatarUrl);
              } catch {
                console.warn("Invalid userInfo in localStorage");
              }
            }
          } else {
            // access token просрочен — пробуем обновить
            try {
              const res = await axiosInstance.post(
                "/auth/refresh-token",
                {},
                { withCredentials: true }
              );
              const { accessToken, userInfo } = res.data;
              saveTokens(accessToken);
              setAuthState(accessToken, userInfo);
              console.log("Refreshed token on init");
            } catch (refreshError) {
              console.error("Refresh token failed:", refreshError);
              logout();
            }
          }
        } catch (error) {
          console.error("Invalid token:", error);
          localStorage.clear();
        }
      }
      setIsAuthLoading(false);
    };

    initializeAuth();
  }, []);

  const setAuthState = (
    token: string | null,
    userInfo?: UserAccountInfoDto | null
  ) => {
    if (token && userInfo) {
      localStorage.setItem("userInfo", JSON.stringify(userInfo));
      try {
        const decoded: any = jwtDecode(token);
        console.log("Decoded JWT in setAuthState:", decoded);
        setUserId(extractUserId(decoded));
        setRole(extractUserRole(decoded));
        setUserName(userInfo.userName);
        setAvatarUrl(userInfo.avatarUrl);
        setIsAuthenticated(true);
        console.log("User info: ", userInfo);
      } catch (error) {
        console.error("Invalid token:", error);
      }
    } else {
      localStorage.removeItem("userInfo");
      setUserId(null);
      setRole(null);
      setUserName(null);
      setAvatarUrl(null);
      setIsAuthenticated(false);
    }
  };

  // Функция logout
  const logout = () => {
    stopNotificationConnection(); // Отключаем SignalR при logout
    localStorage.clear();
    setUserId(null);
    setRole(null);
    setUserName(null);
    setAvatarUrl(null);
    setIsAuthenticated(false);
  };

  return (
    <AuthContext.Provider
      value={{
        userId,
        role,
        avatarUrl,
        userName,
        isAuthenticated,
        isAuthLoading,
        setAuthState,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within an AuthProvider");
  return context;
};
