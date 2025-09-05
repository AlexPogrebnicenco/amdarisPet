import axiosInstance from "../api/axios";
import { saveTokens } from "./tokenService";

/**
 * --- DTOs ---
 */

// Used for registering both regular Users and Teachers
export interface RegisterDto {
  userName: string;
  email: string;
  password: string;
  confirmPassword: string;
  age: number;
  gender: string;
  role: string; // "User" or "Teacher"
}

// Backend response with JWT tokens
interface UserAccountInfoDto {
  userName: string;
  avatarUrl: string | null;
}

interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  accessTokenExpiration: string;
  refreshTokenExpiration: string;
  userInfo: UserAccountInfoDto;
}



// Used when Teacher registers — no password yet
export type RegisterTeacherDto = Omit<RegisterDto, "password" | "confirmPassword">;

// DTO for setting a password via email link
interface SetPasswordDto {
  token: string;
  password: string;
  confirmPassword: string;
}

// DTO for Login
interface LoginDto {
  email: string;
  password: string;
}

// Тип функции setAuthState с аватаром
type SetAuthStateFn = (token: string | null, userInfo?: UserAccountInfoDto | null) => void;

/**
 * --- Services ---
 */

// Register a regular user and immediately store JWT tokens
export const registerUser = async (
  data: RegisterDto,
  setAuthState: SetAuthStateFn
): Promise<void> => {
  const response = await axiosInstance.post<AuthResponse>("/auth/register-user", data);
  const { accessToken, userInfo } = response.data;
  saveTokens(accessToken);
  setAuthState(accessToken, userInfo );
};

// Register a teacher without password — waits for admin approval
export const registerTeacher = async (data: RegisterTeacherDto): Promise<void> => {
  await axiosInstance.post("/teacher-requests", data);
};

// Set teacher password after approval — store JWT tokens after success
export const setPassword = async (
  data: SetPasswordDto,
  setAuthState: SetAuthStateFn
): Promise<void> => {
  const response = await axiosInstance.post<AuthResponse>("/auth/set-password", data);
  const { accessToken, userInfo } = response.data;
  saveTokens(accessToken);
  setAuthState(accessToken, userInfo);
};

// Login phase
export const login = async (
  data: LoginDto,
  setAuthState: SetAuthStateFn
): Promise<void> => {
  const response = await axiosInstance.post<AuthResponse>("/auth/login", data);
  const { accessToken, userInfo } = response.data;
  saveTokens(accessToken);
  setAuthState(accessToken, userInfo );
};

// Forgot password request (reset password)
export const forgotPassword = async (email: string): Promise<void> => {
  await axiosInstance.post("/auth/forgot-password", { email });
};

// Request new link (initial password setup)
export const requestNewLink = async (email: string): Promise<void> => {
  await axiosInstance.post("/auth/request-new-link", { email });
};
