import { Button, useTheme, type SxProps } from "@mui/material";

interface CustomButtonProps {
  type?: "button" | "submit" | "reset";
  children: React.ReactNode;
  fullWidth?: boolean;
  onClick?: () => void;
  sx?: SxProps;
}

const CommonButton = ({
  type = "button",
  children,
  fullWidth = false,
  onClick,
  sx,
}: CustomButtonProps) => {
  const theme = useTheme();
  return (
    <Button
      type={type}
      variant="contained"
      fullWidth={fullWidth}
      onClick={onClick}
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
