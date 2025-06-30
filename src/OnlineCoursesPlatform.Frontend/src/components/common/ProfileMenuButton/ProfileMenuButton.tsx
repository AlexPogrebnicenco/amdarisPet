import { IconButton } from "@mui/material";
import AccountCircle from "@mui/icons-material/AccountCircle";
import { useTheme } from "@mui/material/styles";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const ProfileMenuButton = () => {
  const theme = useTheme();
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();

  const handleClick = () => {
    setOpen(true);
    navigate("/app/profile"); 
  };

  const handleBlur = () => {
    setTimeout(() => setOpen(false), 150);
  };

  return (
    <IconButton
      size="large"
      edge="end"
      aria-label="account of current user"
      aria-haspopup="true"
      onClick={handleClick}
      onBlur={handleBlur}
      sx={{
        color: open ? theme.palette.text.secondary : theme.palette.text.primary,
        transform: open ? "scale(1.1)" : "scale(1)",
        transition: "all 0.2s ease-in-out",
        "&:hover": {
          color: theme.palette.text.secondary,
          transform: "scale(1.1)",
        },
      }}
    >
      <AccountCircle />
    </IconButton>
  );
};

export default ProfileMenuButton;
