import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { loginSchema } from "./validationSchema";
import {
  Box,
  IconButton,
  InputAdornment,
  Typography,
  useTheme,
} from "@mui/material";
import CustomLink from "../../../components/common/CustomLink/CustomLink";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import { useState } from "react";
import VisibilityTwoToneIcon from "@mui/icons-material/VisibilityTwoTone";
import VisibilityOffTwoToneIcon from "@mui/icons-material/VisibilityOffTwoTone";
import GoogleIcon from "@mui/icons-material/Google";

type LoginFormInputs = {
  email: string;
  password: string;
};

interface LoginFormProps {
  onSuccess: (formData: { email: string; password: string }) => void;
}

const LoginForm: React.FC<LoginFormProps> = ({ onSuccess }) => {
  const theme = useTheme();
  const [showPassword, setShowPassword] = useState(false);
  const toggleShowPassword = () => setShowPassword((prev) => !prev);

  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    watch,
  } = useForm<LoginFormInputs>({
    resolver: yupResolver(loginSchema),
  });

  const handleGoogleLogin = () => {
    window.location.href = "https://localhost:7025/api/auth/login/google";
  };

  const onSubmit = (data: LoginFormInputs) => {
    onSuccess(data);
  };

  const passwordValue = watch("password");
  const emailValue = watch("email");
  return (
    <Box
      component="form"
      name="login"
      autoComplete="on"
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
        SIGN IN
      </Typography>

      <Typography variant="body2" align="center">
        Don&apos;t have an account?{" "}
        <CustomLink to="/register">Register</CustomLink>
      </Typography>

      <CustomTextField
        label="Email"
        autoComplete="email"
        type="email"
        {...register("email")}
        error={!!errors.email}
        helperText={errors.email?.message}
        sx={{ backgroundColor: emailValue ? "#1A1D20" : "inherit" }}
      />

      <CustomTextField
        label="Password"
        autoComplete="current-password"
        type={showPassword ? "text" : "password"}
        {...register("password")}
        error={!!errors.password}
        helperText={errors.password?.message}
        InputProps={{
          endAdornment: (
            <InputAdornment position="end">
              <IconButton onClick={toggleShowPassword} edge="end">
                {showPassword ? (
                  <VisibilityOffTwoToneIcon sx={{ color: "#0C85C6" }} />
                ) : (
                  <VisibilityTwoToneIcon sx={{ color: "#0369A1" }} />
                )}
              </IconButton>
            </InputAdornment>
          ),
        }}
        sx={{ backgroundColor: passwordValue ? "#1A1D20" : "inherit" }}
      />
        <Typography variant="body2" textAlign={"center"} >
        Forgot your password?{" "}
        <CustomLink to="/auth/forgot-password">Reset here</CustomLink>
      </Typography>

      <Box
        sx={{
          position: "sticky",
          bottom: 0,
          backgroundColor: theme.palette.background.paper,
          pb: 2,
          mt: 1,
          zIndex: 1,
        }}
      >
        <CommonButton type="submit" fullWidth>
          SIGN IN
        </CommonButton>
         <CommonButton
        fullWidth
        onClick={handleGoogleLogin}
        startIcon={<GoogleIcon />}
        sx={{
          mt: 2,
          backgroundColor: "#DB4437",
          "&:hover": { backgroundColor: "#c1351d" },
        }}
      >
        Sign in with Google
      </CommonButton>
      </Box>
    
    </Box>
  );
};

export default LoginForm;
