// src/pages/Auth/ForgotPassword/ForgotPasswordPage.tsx
import { Box, Paper, Typography, useTheme } from "@mui/material";
import { useForm } from "react-hook-form";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import { toast } from "react-toastify";
import { forgotPassword } from "../../../services/authService";
import { useNavigate } from "react-router-dom";

interface ForgotPasswordDto {
  email: string;
}

const ForgotPasswordPage = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<ForgotPasswordDto>();

  const onSubmit = async (data: ForgotPasswordDto) => {
    try {
      await forgotPassword(data.email);
      toast.success("Password reset link has been sent to your email.");
      reset();
      navigate("/login");
    } catch (error: any) {
      console.error("Error requesting password reset", error);
      toast.error(
        error.response?.data?.detail ||
          "Something went wrong. Please try again."
      );
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
        <Box
          component="form"
          onSubmit={handleSubmit(onSubmit)}
          sx={{
            display: "flex",
            flexDirection: "column",
            gap: 2,
            width: "100%",
            maxWidth: 400,
            height: "100%",
            justifyContent: "center",
            mx: "auto",
          }}
        >
          <Typography
            variant="h5"
            align="center"
            sx={{
              color: theme.palette.text.secondary,
              fontFamily: "'Audiowide', cursive",
              mb: 2,
            }}
          >
            Forgot Password
          </Typography>

          <CustomTextField
            label="Email"
            type="email"
            {...register("email", { required: "Email is required" })}
            error={!!errors.email}
            helperText={errors.email?.message}
          />

          <Box
            sx={{
              position: "sticky",
              bottom: 0,
              backgroundColor: theme.palette.background.paper,
              pt: 2,
              pb: 2,
              mt: 2,
              zIndex: 1,
            }}
          >
            <CommonButton type="submit" fullWidth>
              Send Reset Link
            </CommonButton>
          </Box>
        </Box>
      </Paper>
    </Box>
  );
};

export default ForgotPasswordPage;
