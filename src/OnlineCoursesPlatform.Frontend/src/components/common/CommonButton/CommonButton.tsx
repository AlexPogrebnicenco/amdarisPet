import { Button, useTheme, type SxProps } from "@mui/material";
import type { ReactNode } from "react";

interface CustomButtonProps {
  type?: "button" | "submit" | "reset";
  children: React.ReactNode;
  fullWidth?: boolean;
  onClick?: () => void;
  sx?: SxProps;
  startIcon?: ReactNode;
  disabled?: boolean;
}

const CommonButton = ({
  type = "button",
  children,
  fullWidth = false,
  onClick,
  sx,
  startIcon,
  disabled,
}: CustomButtonProps) => {
  const theme = useTheme();
  return (
    <Button
      type={type}
      variant="contained"
      fullWidth={fullWidth}
      onClick={onClick}
      startIcon={startIcon}
      disabled={disabled}
      sx={{
        backgroundColor: theme.palette.button.main,
        color: "#fff",
        textTransform: "none",
        fontWeight: 500,
        transition: "background-color 0.2s, transform 0.1s",
        "&:hover": {
          backgroundColor: theme.palette.button.hover,
        },
        "&:active": {
          transform: "scale(0.98)",
        },
        ...sx,
      }}
    >
      {children}
    </Button>
  );
};

export default CommonButton;
