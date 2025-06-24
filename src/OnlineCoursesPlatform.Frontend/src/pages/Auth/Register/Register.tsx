import { Box, useTheme, Paper } from "@mui/material";
import RegisterForm from "./RegisterForm";

const Register = () => {
  const theme = useTheme();
  return (
    <Box
      sx={{
        backgroundColor: theme.palette.background.default,
        minHeight: "100vh",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        px: 2,
        // py: { xs: 6, sm: 10, md: 14 }, // адаптивные отступы сверху и снизу
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
        <RegisterForm />
      </Paper>
    </Box>
  );
};

export default Register;
