import { IconButton, Avatar, useTheme, Tooltip } from "@mui/material";
import AccountCircle from "@mui/icons-material/AccountCircle";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../../context/AuthContext";

const ProfileMenuButton = () => {
  const theme = useTheme();
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();
  const { avatarUrl } = useAuth(); 

  const handleClick = () => {
    setOpen(true);
    navigate("/app/profile");
  };

  const handleBlur = () => {
    setTimeout(() => setOpen(false), 150);
  };

  return (
    <Tooltip title="Go to profile">
      <IconButton
        size="large"
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
          p: 0.5,
        }}
      >
        {avatarUrl ? (
          <Avatar
            src={avatarUrl}
            alt="User Avatar"
            sx={{
              width: 28,
              height: 28,
              transition: "all 0.2s ease-in-out",
            }}
          />
        ) : (
          <AccountCircle
            sx={{
              width: 28,
              height: 28,
              transition: "all 0.2s ease-in-out",
            }}
          />
        )}
      </IconButton>
    </Tooltip>
  );
};

export default ProfileMenuButton;
