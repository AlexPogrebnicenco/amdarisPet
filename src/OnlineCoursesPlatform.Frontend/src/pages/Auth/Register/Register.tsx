import { Box, useTheme, Paper } from "@mui/material";
import RegisterForm from "./RegisterForm";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../../context/AuthContext";
import { registerUser, registerTeacher } from "../../../services/authService";

const Register = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const { setAuthState } = useAuth();

  const handleRegister = async (formData: any) => {
    try {
      if (formData.role === "Teacher") {
        const { password, confirmPassword, ...teacherData } = formData;
        await registerTeacher(teacherData);
        alert("Teacher registration application sent. Wait for confirmation.");
        navigate("/");
      } else {
        await registerUser(formData, setAuthState);
        navigate("/app/home");
      }
    } catch (error) {
      console.error("Registration error:", error);
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
        <RegisterForm onSuccess={handleRegister} />
      </Paper>
    </Box>
  );
};

export default Register;
