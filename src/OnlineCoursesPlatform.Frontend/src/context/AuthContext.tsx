import { createContext, useContext, useEffect, useState } from "react";
import type { ReactNode } from "react";
import { jwtDecode } from "jwt-decode";

interface AuthContextType {
  role: string | null;
  isAuthenticated: boolean;
  isAuthLoading: boolean;
  setAuthState: (token: string | null) => void;
  logout: () => void; // Добавляем logout в интерфейс
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const extractUserRole = (decodedJwt: any) => {
  return decodedJwt["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
};

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [role, setRole] = useState<string | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [isAuthLoading, setIsAuthLoading] = useState(true);

  // Инициализация при загрузке страницы
  useEffect(() => {
    const token = localStorage.getItem("accessToken");
    if (token) {
      try {
        const decoded: any = jwtDecode(token);
        console.log("Decoded JWT on init:", decoded);
        const currentTime = Date.now() / 1000;

        if (decoded.exp && decoded.exp > currentTime) {
          setRole(extractUserRole(decoded));
          setIsAuthenticated(true);
        } else {
          localStorage.clear();
        }
      } catch (error) {
        console.error("Invalid token:", error);
        localStorage.clear();
      }
    }
    setIsAuthLoading(false);
  }, []);

  const setAuthState = (token: string | null) => {
    if (token) {
      try {
        const decoded: any = jwtDecode(token);
        console.log("Decoded JWT in setAuthState:", decoded);
        setRole(extractUserRole(decoded));
        setIsAuthenticated(true);
      } catch (error) {
        console.error("Invalid token:", error);
      }
    } else {
      setRole(null);
      setIsAuthenticated(false);
    }
  };

  // Функция logout
  const logout = () => {
    localStorage.clear(); // Очищаем localStorage
    setRole(null); // Сбрасываем роль
    setIsAuthenticated(false); // Сбрасываем авторизацию
  };

  return (
    <AuthContext.Provider
      value={{ role, isAuthenticated, isAuthLoading, setAuthState, logout }}
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
