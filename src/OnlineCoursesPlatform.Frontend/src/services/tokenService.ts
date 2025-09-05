const ACCESS_TOKEN_KEY = "accessToken";
// const REFRESH_TOKEN_KEY = "refreshToken";

// Получить токены
export const getAccessToken = (): string | null => localStorage.getItem(ACCESS_TOKEN_KEY);
// export const getRefreshToken = (): string | null => localStorage.getItem(REFRESH_TOKEN_KEY);

// Сохранить токены
export const saveTokens = (accessToken: string): void => {
  localStorage.setItem(ACCESS_TOKEN_KEY, accessToken);
  // if (refreshToken) {
  //   localStorage.setItem(REFRESH_TOKEN_KEY, refreshToken);
  // }
};

// Очистить токены (logout)
export const clearTokens = (): void => {
  localStorage.removeItem(ACCESS_TOKEN_KEY);
  // localStorage.removeItem(REFRESH_TOKEN_KEY);
};
