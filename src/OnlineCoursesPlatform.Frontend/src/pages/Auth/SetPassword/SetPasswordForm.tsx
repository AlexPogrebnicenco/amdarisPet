import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { setPasswordSchema } from "./validationSchema";
import { Box, Paper, Typography, useTheme } from "@mui/material";
import { useNavigate, useSearchParams } from "react-router-dom";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import { setPassword } from "../../../services/authService";
import type { InferType } from "yup";
import { useAuth } from "../../../context/AuthContext";
import { toast } from "react-toastify";
import CustomLink from "../../../components/common/CustomLink/CustomLink";

type SetPasswordInputs = InferType<typeof setPasswordSchema>;

const SetPasswordForm = () => {
  const theme = useTheme();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token");
  const { setAuthState } = useAuth();

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
      toast.error("Token not found in URL");
      return;
    }

    try {
      await setPassword({ ...data, token }, setAuthState);
      toast.success("Password successfully set!");
      navigate("/app/home");
    } catch (error: any) {
      console.error("Error setting password", error);

      const errorMessage = error.response?.data?.detail;

      if (errorMessage?.includes("already been used")) {
        toast.error(
          "This link has already been used. Please request a new one."
        );
      } else if (errorMessage?.includes("expired")) {
        toast.error("This link has expired. Please request a new one.");
      } else {
        toast.error(errorMessage || "Something went wrong. Please try again.");
      }
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
          <Box
            sx={{
              mt: 2,
              textAlign: "center",
            }}
          >
            <Typography
              variant="body2"
              sx={{ color: theme.palette.text.secondary }}
            >
              Link expired or already used?{" "}
              <CustomLink to="/auth/request-new-link">
                Request a new one
              </CustomLink>
            </Typography>
          </Box>
        </Box>
      </Paper>
    </Box>
  );
};

export default SetPasswordForm;
