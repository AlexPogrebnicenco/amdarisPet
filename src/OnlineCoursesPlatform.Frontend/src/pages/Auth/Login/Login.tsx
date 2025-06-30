import { Box, useTheme, Paper } from "@mui/material";
import LoginForm from "./LoginForm";
import { useNavigate, useLocation } from "react-router-dom";
import { useAuth } from "../../../context/AuthContext";
import { login } from "../../../services/authService";

const Login = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const location = useLocation();
  const { setAuthState } = useAuth();
  const from = location.state?.from?.pathname || "/app/home"; // Редиректим на защищённую home страницу

  const handleLogin = async (formData: { email: string; password: string }) => {
    try {
      await login(formData, setAuthState);
      navigate(from);
    } catch (error) {
      console.error('Login failed', error);
    }
  };

  return (
    <Box
      sx={{
        backgroundColor: theme.palette.background.default,
        minHeight: "100vh",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        px: 2,
        boxSizing: "border-box",
      }}
    >
      <Paper
        elevation={6}
        sx={{
          p: { xs: 3, sm: 4 },
          width: "100%",
          maxWidth: 460,
          backgroundColor: theme.palette.background.paper,
          borderRadius: 3,
          boxShadow: "0 8px 24px rgba(0, 0, 0, 0.3)",
        }}
      >
        <LoginForm onSuccess={handleLogin} />
      </Paper>
    </Box>
  );
};

export default Login;
