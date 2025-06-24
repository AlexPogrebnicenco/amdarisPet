import { Box, useTheme, Paper } from "@mui/material";
import LoginForm from "./LoginForm";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import MatrixRain from "../../../components/common/MatrixRain/MatrixRain";
import { useLocation } from "react-router-dom";

const Login = () => {
  const theme = useTheme();
  const [showMatrix, setShowMatrix] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";

  const handleLogin = async () => {
    setShowMatrix(true);
    setTimeout(() => {
      navigate(from); 
    }, 5000);
  };

  if (showMatrix) return <MatrixRain />;

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
