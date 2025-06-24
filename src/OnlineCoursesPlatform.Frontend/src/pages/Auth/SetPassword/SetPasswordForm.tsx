import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { setPasswordSchema } from "./validationSchema";
import { Box, Paper, Typography, useTheme } from "@mui/material";
import { useNavigate, useSearchParams } from "react-router-dom";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import { setPassword } from "../../../services/authService";
import type { InferType } from "yup";

type SetPasswordInputs = InferType<typeof setPasswordSchema>;

const SetPasswordForm = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token");

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<SetPasswordInputs>({
    resolver: yupResolver(setPasswordSchema),
  });

  const onSubmit = async (data: SetPasswordInputs) => {
    if (!token) {
      console.error("Token not found in URL");
      return;
    }

    try {
      await setPassword({ ...data, token });
      navigate("/");
    } catch (error) {
      console.error("Error setting password", error);
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
            Set Your Password
          </Typography>

          <CustomTextField
            label="Password"
            type="password"
            {...register("password")}
            error={!!errors.password}
            helperText={errors.password?.message}
          />

          <CustomTextField
            label="Confirm Password"
            type="password"
            {...register("confirmPassword")}
            error={!!errors.confirmPassword}
            helperText={errors.confirmPassword?.message}
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
              Set Password
            </CommonButton>
          </Box>
        </Box>
      </Paper>
    </Box>
  );
};

export default SetPasswordForm;
