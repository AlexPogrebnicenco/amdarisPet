import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { loginSchema } from "./validationSchema";
import { Box, Typography, useTheme } from "@mui/material";
import CustomLink from "../../../components/common/CustomLink/CustomLink";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";

type LoginFormInputs = {
  email: string;
  password: string;
};

interface LoginFormProps {
  onSuccess: (formData: { email: string; password: string }) => void;
}

const LoginForm: React.FC<LoginFormProps> = ({ onSuccess }) => {
  const theme = useTheme();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormInputs>({
    resolver: yupResolver(loginSchema),
  });

  const onSubmit = (data: LoginFormInputs) => {
    onSuccess(data);
  };

  return (
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
        SIGN IN
      </Typography>

      <CustomTextField
        label="Email"
        type="email"
        {...register("email")}
        error={!!errors.email}
        helperText={errors.email?.message}
      />

      <CustomTextField
        label="Password"
        type="password"
        {...register("password")}
        error={!!errors.password}
        helperText={errors.password?.message}
      />

      <Box
        sx={{
          position: "sticky",
          bottom: 0,
          backgroundColor: theme.palette.background.paper,
          pt: 2,
          pb: 2,
          mt: 1,
          zIndex: 1,
        }}
      >
        <CommonButton type="submit" fullWidth>
          SIGN IN
        </CommonButton>
      </Box>
      <Typography variant="body2" align="center" sx={{ mt: 2 }}>
        Forgot your password?{" "}
        <CustomLink to="/auth/forgot-password">Reset here</CustomLink>
      </Typography>
      <Typography variant="body2" align="center">
        Don&apos;t have an account?{" "}
        <CustomLink to="/register">Register here</CustomLink>
      </Typography>
    </Box>
  );
};

export default LoginForm;
