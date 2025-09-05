import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { registerSchema } from "./validationSchema";
import {
  Box,
  Typography,
  MenuItem,
  useTheme,
  Autocomplete,
} from "@mui/material";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import type { InferType } from "yup";
import CustomAutocomplete from "../../../components/common/CustomAutocomplete/CustomAutocomplete";

const roles = [
  { id: 1, label: "User" },
  { id: 2, label: "Teacher" },
];

const genders = [
  { id: 1, label: "Male" },
  { id: 2, label: "Female" },
];

type RegisterFormInputs = InferType<typeof registerSchema>;

interface RegisterFormProps {
  onSuccess: (formData: RegisterFormInputs) => void;
}

const RegisterForm = ({ onSuccess }: RegisterFormProps) => {
  const theme = useTheme();

  const {
    register,
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<RegisterFormInputs>({
    resolver: yupResolver(registerSchema),
  });

  const onSubmit = (data: RegisterFormInputs) => {
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
        CREATE YOUR ACCOUNT
      </Typography>

      <CustomTextField
        label="Username"
        {...register("userName")}
        error={!!errors.userName}
        helperText={errors.userName?.message}
      />

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

      <CustomTextField
        label="Confirm Password"
        type="password"
        {...register("confirmPassword")}
        error={!!errors.confirmPassword}
        helperText={errors.confirmPassword?.message}
      />

      <CustomTextField
        label="Age"
        type="number"
        {...register("age")}
        error={!!errors.age}
        helperText={errors.age?.message}
      />

      <Controller
        name="gender"
        control={control}
        render={({ field }) => (
          <CustomAutocomplete
            options={genders}
            getOptionLabel={(option) => option.label}
            value={genders.find((g) => g.label === field.value) || null}
            onChange={(newValue) => field.onChange(newValue?.label ?? "")}
            label="Gender"
            placeholder="Select gender"
            textFieldProps={{
              error: !!errors.gender,
              helperText: errors.gender?.message,
            }}
          />
        )}
      />

      <Controller
        name="role"
        control={control}
        render={({ field }) => (
          <CustomAutocomplete
            options={roles}
            getOptionLabel={(option) => option.label}
            value={roles.find((r) => r.label === field.value) || null}
            onChange={(newValue) => field.onChange(newValue?.label ?? "")}
            label="Role"
            placeholder="Select a role"
            textFieldProps={{
              error: !!errors.role,
              helperText: errors.role?.message,
            }}
          />
        )}
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
          Create account
        </CommonButton>
      </Box>
    </Box>
  );
};

export default RegisterForm;
