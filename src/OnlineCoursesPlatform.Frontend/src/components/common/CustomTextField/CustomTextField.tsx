import { TextField, type TextFieldProps, useTheme } from "@mui/material";

const CustomTextField = (props: TextFieldProps) => {
  const theme = useTheme();

  return (
    <TextField
      fullWidth
      size="medium"
      variant="outlined"
      {...props}
      slotProps={{
        inputLabel: {
          sx: {
            color: theme.palette.text.primary,
            "&.Mui-focused": {
              color: theme.palette.text.secondary,
            },
          },
        },
      }}
      sx={{
        "& .MuiOutlinedInput-root": {
          "& fieldset": {
            borderColor: theme.palette.text.primary,
          },
          "&:hover fieldset": {
            borderColor: theme.palette.text.secondary,
          },
          "&.Mui-focused fieldset": {
            borderColor: theme.palette.text.secondary,
          },
        },
        ...props.sx,
      }}
    />
  );
};

export default CustomTextField;
