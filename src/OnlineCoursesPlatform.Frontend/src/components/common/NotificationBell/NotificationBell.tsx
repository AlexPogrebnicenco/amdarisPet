import React, { useState } from "react";
import Badge from "@mui/material/Badge";
import NotificationIcon from "@mui/icons-material/Notifications";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import BasicMenu from "../BasicMenu/BasicMenu";
import { useTheme } from "@mui/material/styles";

const notifications = [
  {
    id: 0,
    label: "First notification",
  },
  {
    id: 1,
    label: "Second notification",
  },
];

const NotificationBell = () => {
  const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);

  const handleOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
    if (notifications.length > 0) {
      setAnchorEl(event.currentTarget);
    }
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const open = Boolean(anchorEl);
  const tooltipText = notifications.length
    ? `You have ${notifications.length} new notifications!`
    : "No new notifications";
  const theme = useTheme();

  return (
    <>
      <Tooltip title={tooltipText}>
        <IconButton
          size="large"
          aria-label="show notifications"
          onClick={handleOpen}
          sx={{
            color: open
              ? theme.palette.text.secondary
              : theme.palette.text.primary,
            transform: open ? "scale(1.1)" : "scale(1)",
            transition: "all 0.2s ease-in-out",
            "&:hover": {
              color: theme.palette.text.secondary,
              transform: "scale(1.1)",
            },
          }}
        >
          <Badge
            badgeContent={notifications.length}
            sx={{
              "& .MuiBadge-badge": {
                backgroundColor: theme.palette.error.main,
                color: open
                  ? theme.palette.text.secondary
                  : theme.palette.text.primary,
                transition: "color 0.2s ease-in-out",
              },
              "&:hover .MuiBadge-badge": {
                color: theme.palette.text.secondary,
              },
            }}
          >
            <NotificationIcon />
          </Badge>
        </IconButton>
      </Tooltip>
      <BasicMenu
        anchorEl={anchorEl}
        open={open}
        handleClose={handleClose}
        menuItems={notifications}
      />
    </>
  );
};

export default NotificationBell;
